using System;
using System.Net.Sockets;
using System.IO;
using System.Collections.Concurrent;
using S = ServerPackets;
using C = ClientPackets;
using System.Collections.Generic;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using Server.MirObjects;

namespace Server.MirNetwork
{
    public class ServerManager
    {
        public readonly string IPAddress;
        private TcpClient _client;
        private ConcurrentQueue<Packet> _receiveList;
        private ConcurrentQueue<Packet> _sendList, _retryList;
        public bool DebugMode = Settings.DebugServerManager;
        public bool LoggedIn = false;
        private bool _disconnecting;
        public bool Connected;

        public string ManagerUser;

        public bool Disconnecting
        {
            get { return _disconnecting; }
            set
            {
                if (_disconnecting == value) return;
                _disconnecting = value;
            }
        }
        byte[] _rawData = new byte[0];
        public ServerManager(TcpClient client)
        {
            try
            {
                cpuCounter = new PerformanceCounter();
                cpuCounter.CategoryName = "Processor";
                cpuCounter.CounterName = "% Processor Time";
                cpuCounter.InstanceName = "_Total";
                //Packet.IsServer = true;
                IPAddress = client.Client.RemoteEndPoint.ToString().Split(':')[0];

                _client = client;
                _client.NoDelay = true;
                _receiveList = new ConcurrentQueue<Packet>();
                _sendList = new ConcurrentQueue<Packet>();
                _retryList = new ConcurrentQueue<Packet>();
                _sendList.Enqueue(new S.Connected());
                Connected = true;
                SMain.EnqueueDebugging(string.Format("[Server Manager]{0} Connected", IPAddress));
                BeginReceive();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Settings.LogPath + "Error Log (" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ").txt",
                                           String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, ex.ToString()));
            }
        }

        private void BeginReceive()
        {
            if (!Connected) return;

            byte[] rawBytes = new byte[8 * 1024];

            try
            {
                _client.Client.BeginReceive(rawBytes, 0, rawBytes.Length, SocketFlags.None, ReceiveData, rawBytes);
            }
            catch
            {
                Disconnecting = true;
            }
        }
        private void ReceiveData(IAsyncResult result)
        {
            //SMain.EnqueueDebugging(string.Format("[ServerManager]Data Received Check.."));
            if (!Connected) return;
            if (DebugMode)
                SMain.EnqueueDebugging(string.Format("[ServerManager]Data Received Connected Pass.."));
            int dataRead;

            try
            {
                dataRead = _client.Client.EndReceive(result);
            }
            catch
            {
                Disconnecting = true;
                return;
            }

            if (dataRead == 0)
            {
                Disconnecting = true;
                return;
            }

            byte[] rawBytes = result.AsyncState as byte[];

            byte[] temp = _rawData;
            _rawData = new byte[dataRead + temp.Length];
            Buffer.BlockCopy(temp, 0, _rawData, 0, temp.Length);
            Buffer.BlockCopy(rawBytes, 0, _rawData, temp.Length, dataRead);

            Packet p;
            while ((p = Packet.ReceivePacket(_rawData, out _rawData)) != null)
                _receiveList.Enqueue(p);

            BeginReceive();
        }

        private void BeginSend(List<byte> data)
        {
            if (!Connected || data.Count == 0) return;

            //Interlocked.Add(ref Network.Sent, data.Count);

            try
            {
                _client.Client.BeginSend(data.ToArray(), 0, data.Count, SocketFlags.None, SendData, Disconnecting);
            }
            catch
            {
                Disconnecting = true;
            }
        }

        private void SendData(IAsyncResult result)
        {
            try
            {
                _client.Client.EndSend(result);
            }
            catch
            { }
        }

        public void Enqueue(Packet p)
        {
            if (_sendList != null && p != null)
                _sendList.Enqueue(p);
        }

        
        
        public long processMsgTime;
        public long playerListCounter;
        public void Process()
        {
            if (SMain.Envir.Time > processMsgTime)
            {
                processMsgTime = SMain.Envir.Time + 1500;
                Enqueue(new S.KeepAlive { Time = SMain.Envir.Time });
            }
            if (SMain.Envir.Running)
            {
                if (SMain.Envir.Time > playerListCounter)
                {
                    playerListCounter = SMain.Envir.Time + Settings.Minute;
                    if (SMain.Envir.Players == null ||
                        SMain.Envir.Players.Count <= 0)
                    {

                    }
                    else
                    {
                        List<ToolOnlinePlayerListItem> players = new List<ToolOnlinePlayerListItem>();

                        for (int i = 0; i < SMain.Envir.Players.Count; i++)
                        {
                            PlayerObject pOb = SMain.Envir.Players[i];
                            players.Add(new ToolOnlinePlayerListItem
                            {
                                PlayerName = pOb.Name,
                                PlayerGuild = pOb.MyGuild != null ? pOb.MyGuild.Name : "NONE",
                                PlayerLevel = pOb.Level,
                                PlayerCurrentMap = pOb.CurrentMap.Info.Title,
                                PlayerCurrentLocation = string.Format("X:[{0}] Y:[{1}]", pOb.CurrentLocation.X, pOb.CurrentLocation.Y),
                                AccountGold = pOb.Account.Gold,
                                AccountCredit = pOb.Account.Credit

                            });
                        }
                        if (players != null &&
                            players.Count > 0)
                        {
                            Enqueue(new S.ToolPlayerList { Players = players });
                        }
                    }

                }
            }
            if (_client == null || !_client.Connected || _receiveList == null || _retryList == null)
            {
                Disconnect();
                return;
            }

            while (!_receiveList.IsEmpty && !Disconnecting)
            {
                if (DebugMode)
                    SMain.EnqueueDebugging(string.Format("[ServerManager][IN]Processing.."));
                if (!_receiveList.TryDequeue(out Packet p)) continue;
                ProcessPacket(p);
            }

            while (_retryList.Count > 0)
            {
                if (!_retryList.TryDequeue(out Packet p) || p == null) continue;
                _receiveList.Enqueue(p);
            }

            if (_sendList == null || _sendList.Count <= 0) return;

            List<byte> data = new List<byte>();
            while (_sendList.Count > 0)
            {
                if (DebugMode)
                    SMain.EnqueueDebugging(string.Format("[ServerManager][OUT]Processing.."));
                if (!_sendList.TryDequeue(out Packet p) || p == null) continue;
                data.AddRange(p.GetPacketBytes());
            }
            //BeginReceive();
            BeginSend(data);
        }
        private static PerformanceCounter cpuCounter;
        public static string CurrentCPUusage
        {
            get
            {
                float temp = 100.0f - cpuCounter.NextValue();
                return string.Format("{0:###.##}%", temp);
            }
        }
        private void ProcessPacket(Packet p)
        {
            if (DebugMode)
                SMain.EnqueueDebugging(string.Format("Packet Received {0}", p.Index));
            if (p == null || Disconnecting) return;
            switch(p.Index)
            {
                case (short)ClientPacketIds.ExternalToolLogin:
                    if (DebugMode)
                        SMain.EnqueueDebugging(string.Format("{0} logging in...", IPAddress));
                    CheckExternalLogin((C.ExternalToolLogin)p);
                    break;
                case (short)ClientPacketIds.Disconnect:
                    SMain.EnqueueDebugging(string.Format("{0} disconnected.", ManagerUser));
                    Disconnect();
                    break;
                case (short)ClientPacketIds.KeepAlive:
                    C.KeepAlive keepAlive = (C.KeepAlive)p;
                    if (DebugMode)
                        SMain.EnqueueDebugging(string.Format("Keep Alive {0}", keepAlive.Time));
                    break;
                case (short)ClientPacketIds.ToolStats:
                    if (LoggedIn)
                        GetStats();
                    break;
                case (short)ClientPacketIds.Chat:
                    if (LoggedIn)
                    {
                        C.Chat chat = (C.Chat)p;
                        string[] splits = chat.Message.Split(' ');
                        if (splits.Length == 0)
                        {
                            Enqueue(new S.Chat { Message = string.Format("Invalid Input"), Type = ChatType.System });
                            break;
                        }
                        else
                        {
                            if (splits[0].StartsWith("@"))
                            {
                                ReceiveCommand((C.Chat)p);
                            }
                            else if (splits[0].StartsWith("/"))
                            {
                                if (SMain.Envir.Running)
                                    MessageToPlayer((C.Chat)p);
                            }
                            else if (splits[0].StartsWith("!"))
                            {
                                if (SMain.Envir.Running)
                                {
                                    BroadcastMessage((C.Chat)p);
                                }
                            }
                            break;
                        }
                    }
                    break;
                default:
                    SMain.EnqueueDebugging(string.Format("Invalid Packet for Server Manager\nIndex : {0}", p.Index));
                    break;
            }
        }

        public void MessageToPlayer(C.Chat p)
        {
            string playerName = "";
            playerName = p.Message.Replace("/", "");
            string[] split = playerName.Split(' ');
            if (split.Length <= 1)
                return;
            playerName = split[0];
            string message = "SYSTEM";
            for (int i = 1; i < split.Length; i++)
            {
                message += string.Format(" {0}", split[i]);
            }
            PlayerObject player = SMain.Envir.GetPlayer(playerName);
            if (player == null)
            {
                Enqueue(new S.Chat { Message = string.Format("{0} could not be found", playerName), Type = ChatType.System });
                Enqueue(new S.Chat { Message = string.Format("{0} could not be found", playerName), Type = ChatType.WhisperOut });
                if (Settings.DebugServerManager)
                    SMain.EnqueueDebugging(string.Format("{0} could not be found.", playerName));
                return;
            }
            if (player.Connection != null &&
                player.Connection.Connected &&
                player.Connection.Stage == GameStage.Game)
            {
                if (Settings.DebugServerManager)
                    SMain.EnqueueDebugging(string.Format("Messenger > {0} : {1}", playerName, message));
                player.ReceiveChat(message, ChatType.WhisperIn);
                Enqueue(new S.Chat { Message = message, Type = ChatType.WhisperOut });
            }
        }

        public void BroadcastMessage(C.Chat p)
        {
            SMain.EnqueueDebugging(string.Format("{0}.", p.Message));
            SMain.Envir.Broadcast(new S.Chat { Message = p.Message.Replace("!", "(!)"), Type = ChatType.Announcement });
        }

        public void ReceiveCommand(C.Chat p)
        {
            if (!LoggedIn)
                return;
            SMain.EnqueueDebugging(string.Format("Command Received : {0}", p.Message));
            if (SMain.Envir.ManagerCommands != null)
            {
                SMain.Envir.ManagerCommands.Add(p.Message);
            }
        }

        public string ProcessCommand(string cmd)
        {
            string retVal = "";

            string[] splits = cmd.Split(' ');
            if (splits.Length >= 1)
            {
                if (splits[0].StartsWith("@"))
                    splits[0] = splits[0].Replace("@", "");
                switch(splits[0])
                {
                    case "DEBUG":
                        if (splits.Length < 2)
                            return retVal;
                        if (splits[1].ToUpper() == "ON")
                            DebugMode = true;
                        else
                            DebugMode = false;
                        Settings.DebugServerManager = DebugMode;
                        Settings.Save();
                        break;
                    case "STOP":
                        if (SMain.Envir.Running)
                            SMain.Envir.Stop();
                        Enqueue(new S.Chat { Message = "Server Stopped", Type = ChatType.System });
                        break;
                    case "START":
                        if (!SMain.Envir.Running)
                            SMain.Envir.Start();
                        Enqueue(new S.Chat { Message = "Server Started", Type = ChatType.System });
                        break;
                    case "RESTART":
                        if (SMain.Envir.Running)
                        {
                            SMain.Envir.Stop();
                            Enqueue(new S.Chat { Message = "Server Stopped", Type = ChatType.System });
                        }
                        SMain.Envir.Start();
                        Enqueue(new S.Chat { Message = "Server Started", Type = ChatType.System });
                        break;
                    case "GIVECREDIT":
                        if (SMain.Envir.Running)
                        {
                            if (splits.Length < 3)
                                return retVal;
                            string pName = splits[1];
                            PlayerObject player = SMain.Envir.GetPlayer(pName);
                            if (player == null)
                            {
                                return retVal;
                            }
                            if (!ushort.TryParse(splits[2], out ushort tmpUS))
                            {
                                return retVal;
                            }
                            player.GainCredit(tmpUS);
                            player.ReceiveChat(string.Format("You have been credited with {0:#,###} credits", tmpUS), ChatType.System);
                        }
                        break;
                    case "KICK":
                        if (SMain.Envir.Running)
                        {
                            if (splits.Length < 2)
                                return retVal;
                            if (splits[1].ToUpper() == "ALL")
                            {
                                for (int i = 0; i < SMain.Envir.Players.Count; i++)
                                {
                                    SMain.Envir.Players[i].Connection.SendDisconnect(4);
                                }
                            }
                            else
                            {
                                string pName = splits[1];
                                PlayerObject player = SMain.Envir.GetPlayer(pName);
                                if (player == null)
                                    return retVal;
                                player.Connection.SendDisconnect(4);
                            }
                        }
                        break;
                    case "HELP":
                        string helpString = string.Format(
                            "--------------------------" +
                            "--\tCommand\t\tParam0\t\tParam1\r\n" +
                            "--\t@DEBUG\t\t[ON]|[OFF]\t\t--\r\n" +
                            "--\t@STOP\t\t--\r\n" +
                            "--\t@START\t\t--\r\n" +
                            "--\t@RESTART\t\t--\r\n" +
                            "--------------------------" +
                            "--\tChats Available\t--\r\n" +
                            "--\tWhisper\t\t/PlayerName Message\t--\r\n" +
                            "--\tAnnounce\t\t!Message Here\t--\r\n" +
                            "--------------------------"
                            );
                        Enqueue(new S.Chat { Message = helpString, Type = ChatType.System });
                        break;
                }
            }
            return retVal;
        }

        public void GetStats()
        {
            if (!LoggedIn)
                return;
            string availCPU = string.Format("{0}", CurrentCPUusage);
            string AvailMem = string.Format("{0}", ConvertToFileSize(new ComputerInfo().AvailablePhysicalMemory));
            Enqueue(new S.ToolStats
            {
                MonsterCount = SMain.Envir.MonsterCount,
                PlayerCount = SMain.Envir.PlayerCount,
                ServerCycles = SMain.Envir.Cycles,
                ServerUpTime = SMain.Envir.Uptime,
                ServerCPU = availCPU,
                ServerRAM = AvailMem,
                ServerRunning = SMain.Envir.Running
            });
        }

        public static string ConvertToFileSize(double fileSize)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (fileSize >= 1024 && order < sizes.Length - 1)
            {
                order++;
                fileSize = fileSize / 1024;
            }
            return string.Format("{0:0.#} {1}", fileSize, sizes[order]);
        }

        public void CheckExternalLogin(C.ExternalToolLogin p)
        {
            if (p.Password.Length < 4 ||
                p.Username.Length < 4)
            {
                Enqueue(new S.ExternalToolLogin { Result = 1 });
                SMain.EnqueueDebugging(string.Format("[Server Manager] {0} login failure -1", IPAddress));
                return;
            }
            if (Settings.ManagerUsers != null &&
                Settings.ManagerUsers.Count > 0)
            {
                byte result = 0;
                bool userNameFound = false;
                for (int i = 0; i < Settings.ManagerUsers.Count; i++)
                {
                    if (userNameFound)
                        continue;
                    if (Settings.ManagerUsers[i].UserName.ToUpper() == p.Username.ToUpper())
                    {
                        userNameFound = true;
                        if (Settings.ManagerUsers[i].Password == p.Password)
                            result = 0;
                        else
                            result = 1;
                    }
                }
                if (!userNameFound)
                    result = 2;

                if (result == 0)
                {
                    LoggedIn = true;
                    ManagerUser = p.Username;
                    Enqueue(new S.ExternalToolLogin { Result = result });
                    SMain.EnqueueDebugging(string.Format("[Server Manager] {0} login success", IPAddress));
                    return;
                }
                else
                {

                    Enqueue(new S.ExternalToolLogin { Result = result });
                    SMain.EnqueueDebugging(string.Format("[Server Manager] {0} Login Failure -{1}", IPAddress, result));
                    return;

                }
            }
            else
            {
                Enqueue(new S.ExternalToolLogin { Result = 3 });
                SMain.EnqueueDebugging(string.Format("[Server Manager] {0} No such user", IPAddress));
                return;
            }
        }
        public void Disconnect()
        {
            LoggedIn = false;
            try
            {
                if (!Connected) return;

                Connected = false;

                lock (SMain.Envir.ServerManagers)
                    SMain.Envir.ServerManagers.Remove(this);

                if (_client != null) _client.Client.Dispose();
                _client = null;
                if (DebugMode)
                    SMain.EnqueueDebugging(string.Format("[ServerManager]Disconnected {0}", IPAddress));
            }
            catch (Exception ex)
            {
                File.AppendAllText(Settings.LogPath + "Error Log (" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ").txt",
                                           String.Format("[{0}]: {1}" + Environment.NewLine, DateTime.Now, ex.ToString()));
            }
        }
        public void SendDisconnect()
        {
            Disconnecting = true;
        }
    }
}
