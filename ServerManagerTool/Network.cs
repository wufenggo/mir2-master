using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using C = ClientPackets;

namespace ServerManagerTool
{
    static class Network
    {
        private static TcpClient _client;
        public static int ConnectAttempt = 0;
        public static bool Connected;


        public static int ServerPort { get; set; } = 4000;
        public static string Username { get; set; } = "miradmin";
        public static string Password { get; set; } = "lomcn-edens-elite";
        public static string ServerIP { get; set; } = "192.168.0.109";

        private static ConcurrentQueue<Packet> _receiveList;
        private static ConcurrentQueue<Packet> _sendList, _retryList;

        static byte[] _rawData = new byte[0];


        public static void Connect()
        {
            if (_client != null)
                Disconnect();

            ConnectAttempt++;

            _client = new TcpClient { NoDelay = true };
            _client.BeginConnect(ServerIP, ServerPort, Connection, null);

        }

        private static void Connection(IAsyncResult result)
        {
            try
            {
                _client.EndConnect(result);

                if (!_client.Connected)
                {
                    Connect();
                    return;
                }

                _receiveList = new ConcurrentQueue<Packet>();
                _sendList = new ConcurrentQueue<Packet>();
                _retryList = new ConcurrentQueue<Packet>();
                _rawData = new byte[0];
                BeginReceive();
            }
            catch (SocketException)
            {
                Connect();
            }
            catch (Exception ex)
            {
                ServerManagerForm.SaveError(ex.ToString());
                Disconnect();
            }
        }
        private static void BeginReceive()
        {
            if (_client == null || !_client.Connected) return;

            byte[] rawBytes = new byte[8 * 1024];

            try
            {
                _client.Client.BeginReceive(rawBytes, 0, rawBytes.Length, SocketFlags.None, ReceiveData, rawBytes);
            }
            catch
            {
                Disconnect();
            }
        }
        private static void ReceiveData(IAsyncResult result)
        {
            if (_client == null || !_client.Connected) return;

            int dataRead;

            try
            {
                dataRead = _client.Client.EndReceive(result);
            }
            catch
            {
                Disconnect();
                return;
            }

            if (dataRead == 0)
            {
                Disconnect();
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

        private static void BeginSend(List<byte> data)
        {
            if (_client == null || !_client.Connected || data.Count == 0) return;

            try
            {
                _client.Client.BeginSend(data.ToArray(), 0, data.Count, SocketFlags.None, SendData, null);
            }
            catch
            {
                Disconnect();
            }
        }
        private static void SendData(IAsyncResult result)
        {
            try
            {
                _client.Client.EndSend(result);
            }
            catch
            { }
        }


        public static void Disconnect()
        {
            if (_client == null) return;

            _client.Close();

            
            Connected = false;
            _sendList = null;
            _client = null;

            _receiveList = null;
            if (ServerManagerForm.SMForm.DebugLogs)
                ServerManagerForm.SMForm.OutputNLogs(string.Format("[Network]Disconnected\r\n"));
        }
        public static long nextProcessConsole;
        public static void Process()
        {
            
            if (_client == null || !_client.Connected)
            {
                if (Connected)
                {
                    while (_receiveList != null && !_receiveList.IsEmpty)
                    {

                        if (!_receiveList.TryDequeue(out Packet p) || p == null) continue;
                        if (!(p is ServerPackets.Disconnect) && !(p is ServerPackets.ClientVersion))                            
                            continue;

                        ServerManagerForm.ProcessPacket(p);
                        _receiveList = null;
                        if (ServerManagerForm.SMForm.DebugLogs)
                            ServerManagerForm.SMForm.OutputNLogs(string.Format("[Network][FATAL][0]Connection Died\r\n"));
                        return;
                    }
                    if (ServerManagerForm.SMForm.DebugLogs)
                        ServerManagerForm.SMForm.OutputNLogs(string.Format("[Network][FATAL][1]Connection Died\r\n"));
                    Disconnect();
                    return;
                }
                return;
            }
            if (ServerManagerForm.Time > nextProcessConsole)
            {
                nextProcessConsole = ServerManagerForm.Time + 1500;
                if (ServerManagerForm.SMForm.DebugLogs)
                    ServerManagerForm.SMForm.OutputNLogs(string.Format("[Network]Processing\r\n"));
                
                Enqueue(new C.KeepAlive { Time = ServerManagerForm.Time });
            }


            while (_receiveList != null && !_receiveList.IsEmpty)
            {
                if (!_receiveList.TryDequeue(out Packet p) || p == null) continue;
                if (ServerManagerForm.SMForm.DebugLogs)
                    ServerManagerForm.SMForm.OutputNLogs(string.Format("[Network][IN]Processing Packets..\r\n"));
                ServerManagerForm.ProcessPacket(p);
            }
            while (_retryList.Count > 0)
            {
                if (!_retryList.TryDequeue(out Packet p) || p == null) continue;
                _receiveList.Enqueue(p);
            }
            if (_sendList == null || _sendList.IsEmpty) return;

            List<byte> data = new List<byte>();
            while (!_sendList.IsEmpty)
            {
                if (!_sendList.TryDequeue(out Packet p)) continue;
                if (ServerManagerForm.SMForm.DebugLogs)
                    ServerManagerForm.SMForm.OutputNLogs(string.Format("[Network][OUT]Processing Packets..\r\n"));
                data.AddRange(p.GetPacketBytes());
            }
            BeginSend(data);
        }

        public static void Enqueue(Packet p)
        {
            if (_sendList != null && p != null)
                _sendList.Enqueue(p);
        }
    }
}
