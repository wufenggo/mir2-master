using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using S = ServerPackets;

namespace ServerManagerTool
{
    public partial class ServerManagerForm : Form
    {
        public static ServerManagerForm SMForm;
        public readonly static Stopwatch Timer = Stopwatch.StartNew();
        public readonly static DateTime StartTime = DateTime.Now;
        public static long Time, OldTime;
        public static DateTime Now { get { return StartTime.AddMilliseconds(Time); } }
        public static bool Connected { get { return Network.Connected; } }
        public static bool _Connected { get { return Connected; } }
        public static int RemainingErrorLogs = 0;
        public bool LoggedIn { get; set; } = false;
        public bool DebugLogs { get; set; }

        public List<Label> threadLabels = new List<Label>();

        public ServerManagerForm()
        {
            InitializeComponent();

            SMForm = this;
            
            usernameBox.Text = Network.Username;
            passwordBox.Text = Network.Password;
            ipBox.Text = Network.ServerIP;
            portBox.Text = Network.ServerPort.ToString();
            Application.Idle += Application_Idle;
            FormClosing += ServerManagerForm_FormClosing;
            int dist = playerCountLbl.Location.Y - serverStatus.Location.Y;
            playerCountLbl.Location = new Point(serverStatus.Location.X, serverStatus.Location.Y + dist);
            mobCountLbl.Location = new Point(serverStatus.Location.X, playerCountLbl.Location.Y + dist);
            Location = new Point(serverStatus.Location.X, mobCountLbl.Location.Y + dist);
            cycleLbl.Location = new Point(serverStatus.Location.X, uptimeLbl.Location.Y + dist);
        }

        private void ServerManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Connected)
            {
                SMForm.Text = string.Format("Server Management Tool : Disconnecting..");
                Network.Enqueue(new ClientPackets.Disconnect());
            }
        }

        public static void ProcessPacket(Packet p)
        {
            if (SMForm.DebugLogs)
                OutputLogs(string.Format("Packet Received : {0}\r\n", p.Index));
            switch (p.Index)
            {
                case (short)ServerPacketIds.ExternalToolLogin:
                    StartMonitoring((S.ExternalToolLogin)p);
                    break;
                case (short)ServerPacketIds.Connected:
                    Network.Connected = true;
                    SMForm.connectionBtn.Text = Connected ? "Disconnect" : "Connect";
                    SMForm.Text = string.Format("Server Management Tool : {0}.", Connected ? "Connected" : "Disconnected");
                    Network.Enqueue(new ClientPackets.ExternalToolLogin { Username = SMForm.usernameBox.Text, Password = SMForm.passwordBox.Text });
                    break;
                case (short)ServerPacketIds.Disconnect: // Disconnected
                    Disconnect((S.Disconnect)p);
                    if (SMForm.DebugLogs)
                    {
                        OutputLogs(string.Format("Server Management Tool : {0}.", Connected ? "Connected" : "Disconnected"));
                    }
                    break;
                case (short)ServerPacketIds.KeepAlive:
                    S.KeepAlive keepAlive = (S.KeepAlive)p;
                    if (SMForm.DebugLogs)
                        OutputLogs(string.Format("Keep Alive.. {0}\r\n", keepAlive.Time));
                    break;
                case (short)ServerPacketIds.ToolStats:
                    UpdateStats((S.ToolStats)p);
                    break;
                case (short)ServerPacketIds.Chat:
                    ChatLogRec((S.Chat)p);
                    break;
                case (short)ServerPacketIds.ToolPlayerList:
                    PlayerList((S.ToolPlayerList)p);
                    break;
                default:
                    SMForm.Text = string.Format("Uknown Packed Received.. {0}\r\n", p.Index);
                    break;
            }
        }

        public static void PlayerList(S.ToolPlayerList p)
        {
            if ((p.Players == null || p.Players.Count <= 0) && 
                SMForm.DebugLogs)
                SMForm.OutputNLogs(string.Format("[PlayersOnline]Empty data\r\n"));

            SMForm.UpdatePlayerList(p);
        }

        public void UpdatePlayerList(S.ToolPlayerList p)
        {
            playerOnlineList.DataSource = null;
            playerOnlineList.Rows.Clear();
            for (int i = 0; i < p.Players.Count; i++)
            {
                playerOnlineList.Rows.Add(
                    p.Players[i].PlayerName, 
                    p.Players[i].PlayerLevel, 
                    p.Players[i].PlayerGuild, 
                    p.Players[i].PlayerCurrentMap, 
                    p.Players[i].PlayerCurrentLocation, 
                    string.Format("{0:#,###,###,###}", p.Players[i].AccountGold),
                    string.Format("{0:#,###,###,###}", p.Players[i].AccountCredit));
            }
        }

        public void OutputNLogs(string text)
        {
            managerConsole.Text += string.Format("{0}", text);
            managerConsole.SelectionStart = managerConsole.TextLength;
            managerConsole.ScrollToCaret();
        }

        public static void ChatLogRec(S.Chat p)
        {
            if (p.Type == ChatType.System)
                OutputLogs(string.Format("{0}\r\n",p.Message));
            else
                OutputChatLogs(p.Message, p.Type);
        }

        public static void OutputChatLogs(string text, ChatType cType = ChatType.Normal)
        {
            SMForm.chatConsole.Text += string.Format("Chat Type : {0} - {1}\r\n", cType.ToString(), text);
            SMForm.chatConsole.SelectionStart = SMForm.chatConsole.TextLength;
            SMForm.chatConsole.ScrollToCaret();
        }

        public static void OutputLogs(string text)
        {
            SMForm.managerConsole.Text += string.Format("{0}", text);
            SMForm.managerConsole.SelectionStart = SMForm.managerConsole.TextLength;
            SMForm.managerConsole.ScrollToCaret();
        }

        public static void UpdateStats(S.ToolStats p)
        {
            int dist = SMForm.playerCountLbl.Location.Y - SMForm.serverStatus.Location.Y;
            SMForm.serverStatus.Text = string.Format("Server Status : {0}", p.ServerRunning ? "Online" : "Offline");
            string tmpMobStr =string.Format("{0:#,###,###}", p.MonsterCount);
            SMForm.playerCountLbl.Text = string.Format("Player Count : {0}", p.ServerRunning ? p.PlayerCount.ToString() : "0");
            SMForm.mobCountLbl.Text = string.Format("Monster Count : {0}", p.ServerRunning ? tmpMobStr : "0");            
            SMForm.uptimeLbl.Text = string.Format("Server Online Counter : {0}", p.ServerRunning ? p.ServerUpTime : "N/A");
            string cycleString = string.Empty;
            int threadCount = 1;
            string[] splits = p.ServerCycles.Split('|');
            int x = SMForm.cycleLbl.Location.X + 72;
            int y = SMForm.cycleLbl.Location.Y - dist;
            //  Dispose existing labels
            if (SMForm.threadLabels != null &&
                SMForm.threadLabels.Count > 0)
            {
                for (int i = SMForm.threadLabels.Count - 1; i >= 0; i--)
                    if (SMForm.threadLabels[i] != null ||
                        !SMForm.threadLabels[i].IsDisposed)
                    {
                        SMForm.threadLabels[i].Dispose();
                        SMForm.threadLabels[i] = null;
                        SMForm.threadLabels.RemoveAt(i);
                    }
            }
            //  Clear the label list
            SMForm.threadLabels.Clear();
            for (int u = 0; u < splits.Length; u++)
            {
                //  Create the Labels
                int tempCycleNo = 0;
                string tempstr = splits[u].Replace("CycleDelays:", "");
                tempstr = tempstr.Replace(":", "");
                if (tempstr.StartsWith(" 00"))
                    tempstr = tempstr.Remove(0, 2);
                else if (tempstr.StartsWith(" 0"))
                    tempstr = tempstr.Remove(0, 1);

                int.TryParse(tempstr, out tempCycleNo);
                Color foreColor = Color.Black;
                if (tempCycleNo < 100)
                {
                    if (tempCycleNo >= 250 && tempCycleNo < 300)
                        foreColor = Color.Red;
                    else if (tempCycleNo >= 200 && tempCycleNo < 250)
                        foreColor = Color.OrangeRed;
                    else if (tempCycleNo >= 150 && tempCycleNo < 200)
                        foreColor = Color.Orange;
                    else if (tempCycleNo >= 100 && tempCycleNo < 150)
                        foreColor = Color.LightGoldenrodYellow;
                    else
                        foreColor = Color.Black;
                    
                }
                cycleString = string.Format("Thread [{0}]      -      [{1}]", threadCount, tempCycleNo);
                Label lbl = new Label()
                {
                    Location = new Point(x, y + dist * threadCount),
                    Parent = SMForm.statsTab,
                    AutoSize = true,
                    Text = cycleString,
                    ForeColor = foreColor
                };
                SMForm.threadLabels.Add(lbl);
                
                threadCount++;
            }
            if (p.ServerRunning)
                SMForm.cycleLbl.Text = "Server Cycles";
            else
                SMForm.cycleLbl.Text = "Server Cycles : N/A";
            
            SMForm.ramLbl.Text = "Available RAM : " + p.ServerRAM;
            SMForm.ramLbl.Location = new Point(SMForm.ramLbl.Location.X, y + dist * threadCount);
            
            SMForm.cpuLbl.Text = "Available CPU : " + p.ServerCPU;
            SMForm.cpuLbl.Location = new Point(SMForm.cpuLbl.Location.X, SMForm.ramLbl.Location.Y + dist);
        }

        public static void StartMonitoring(S.ExternalToolLogin p)
        {
            if (p.Result == 0)
            {
                SMForm.Text = string.Format("Server Management Tool : ({0}) - ({1}).", Connected ? "Connected" : "Disconnected", "Logged In");
                SMForm.OutputNLogs("Logged in.\r\n");
                SMForm.LoggedIn = true;
            }
            else if (p.Result == 1)//   ID or Password too short
            {
                SMForm.OutputNLogs("ID or Password does not match\r\n");
                return;
            }
            else if (p.Result == 2)//   ID does not match
            {
                SMForm.OutputNLogs("ID does not match\r\n");
                return;
            }
            else if (p.Result == 3)//   Password does not match
            {
                SMForm.OutputNLogs("Password does not match\r\n");
                return;
            }
        }

        public static void Disconnect(S.Disconnect p)
        {
            if (Connected)
                Network.Disconnect();
            SMForm.LoggedIn = false;
            MessageBox.Show("Disconnected");
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            try
            {
                while (AppStillIdle)
                {
                    UpdateTime();
                    UpdateEnviroment();
                }

            }
            catch (Exception ex)
            {
                SaveError(ex.ToString());
            }
        }

        public static void SaveError(string ex)
        {
            try
            {
                if (RemainingErrorLogs-- > 0)
                {
                    File.AppendAllText(@".\Error.txt",
                                       string.Format("[{0}] {1}{2}", Now, ex, Environment.NewLine));
                }
            }
            catch
            {
            }
        }

        private static void UpdateTime()
        {
            Time = Timer.ElapsedMilliseconds;
        }

        public void ToggleConnection ()
        {
            if (Connected)
            {
                Network.Enqueue(new ClientPackets.Disconnect());
                SMForm.Text = string.Format("Server Management Tool : ({0}).", Connected ? "Connected" : "Disconnected");
                connectionBtn.Text = "Connect";
                OutputNLogs("Logged out.\r\nDisconnected.\r\n");
            }
            else
                Network.Connect();
        }
        static long NextRefresh;
        private static void UpdateEnviroment()
        {
            if (Time > NextRefresh)
            {
                NextRefresh = Time + 5000;
                Network.Enqueue(new ClientPackets.RequestStats());
            }
            Network.Process();
        }

        private static bool AppStillIdle
        {
            get
            {
                return !PeekMessage(out PeekMsg msg, IntPtr.Zero, 0, 0, 0);
            }
        }

        [SuppressUnmanagedCodeSecurity]
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool PeekMessage(out PeekMsg msg, IntPtr hWnd, uint messageFilterMin,
                                               uint messageFilterMax, uint flags);

        private void notifyBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void notifyBox_KeyUp(object sender, KeyEventArgs e)
        {
            
            bool sendCommand = false;
            if (e.KeyCode == Keys.Enter)
            {
                string input = notifyBox.Text;
                string[] splits = input.Split(' ');
                if (splits[0].StartsWith("@"))
                {
                    string strParese = splits[0].Replace("@", "");
                    //splits[0] = splits[0].Replace("@", "");
                    switch (strParese.ToUpper())
                    {
                        case "DEBUG":
                            if (splits.Length < 2)
                                return;
                            if (splits[1].ToUpper() == "ON" && !DebugLogs)
                            {
                                DebugLogs = true;
                                sendCommand = true;
                            }
                            else if (splits[1].ToUpper() == "OFF" && DebugLogs)
                            {
                                DebugLogs = false;
                                sendCommand = true;
                            }
                            break;
                        case "STOP":
                            sendCommand = true;
                            break;
                        case "START":
                            sendCommand = true;
                            break;
                        case "RESTART":
                            sendCommand = true;
                            break;
                        case "HELP":
                            sendCommand = true;
                            break;
                        case "GIVECREDIT":
                            if (splits.Length < 3)
                                return;
                            else
                                sendCommand = true;
                            break;
                        case "KICK":
                            if (splits.Length < 2)
                                return;
                            else
                                sendCommand = true;
                            break;
                        default:
                            break;
                    }
                    if (sendCommand)
                        Network.Enqueue(new ClientPackets.Chat { Message = input });
                }
                else if (splits[0].StartsWith("/"))
                {
                    Network.Enqueue(new ClientPackets.Chat { Message = input });
                }
                else if (splits[0].StartsWith("!"))
                {
                    Network.Enqueue(new ClientPackets.Chat { Message = input });
                }
                notifyBox.Text = "";
                OutputChatLogs(input.Remove(0, 1));
            }
        }

        private void connectionBtn_Click(object sender, EventArgs e)
        {
            if (!Connected)
            {
                if (ipBox.Text.Length < 6 ||
                ipBox.Text.Length > 253)
                {
                    MessageBox.Show("IP Invalid, check length (too short)");
                    return;
                }
                if (ipBox.Text.Contains("."))
                {
                    IPAddress _ip = Dns.GetHostAddresses(ipBox.Text)[0];
                    if (_ip == null)
                    {
                        MessageBox.Show("IP Invalid");
                        return;
                    }
                }
                if (!int.TryParse(portBox.Text, out int port))
                {
                    MessageBox.Show("Port Invalid.");
                    return;
                }
                if (port < 0 ||
                    port > ushort.MaxValue)
                {
                    MessageBox.Show("Port out of range (0 ~ 65535");
                    return;
                }
                if (usernameBox.Text.Length <= 3 ||
                    usernameBox.Text.Length > 12)
                {
                    if (usernameBox.Text.Length <= 3)
                        MessageBox.Show("Username too short");
                    else
                        MessageBox.Show("Username too long");
                    return;
                }
                if (passwordBox.Text.Length <= 3 ||
                    passwordBox.Text.Length > 17)
                {
                    if (passwordBox.Text.Length <= 3)
                        MessageBox.Show("Password too short");
                    else
                        MessageBox.Show("Password too long");
                    return;
                    //127.0.0.1
                }
                SMForm.Text = string.Format("Server Management Tool : Connecting..");
                Network.ServerIP = ipBox.Text;
                Network.ServerPort = Convert.ToInt32(portBox.Text);
            }
            ToggleConnection();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PeekMsg
        {
            private readonly IntPtr hWnd;
            private readonly Message msg;
            private readonly IntPtr wParam;
            private readonly IntPtr lParam;
            private readonly uint time;
            private readonly Point p;
        }
    }
}
