using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MyNetwork
{
    public class NetworkClient
    {
        #region Public
        public int UID => _UID;
        public IPAddress IP => ((IPEndPoint)_endPoint).Address;
        public int Port => ((IPEndPoint)_endPoint).Port;
        public bool Connected => _connected;
        public ClientData clientData;
        public bool ShowLogs = false;
        public bool ShowLogsTime = false;
        public event ClientHandler? OnClientConnected;
        public event ClientHandler? OnClientDisconnected;
        public event ClientHandler? OnClientDataLoaded;
        public event PacketHandler? OnPacketReceived;
        #endregion

        #region Private
        private int _UID;
        private Socket? _clientSocket;
        private Thread? _clientReceiveThread;
        private EndPoint? _endPoint;
        private bool _connected;
        private bool _onServerClient;
        #endregion

        public NetworkClient() { _connected = false; _onServerClient = false; clientData = new(); }

        public NetworkClient(ClientData data) { _connected = false; clientData = data ?? new(); _onServerClient = false; }

        public NetworkClient(int uid, Socket clientSocket)
        {
            _UID = uid;
            _clientSocket = clientSocket;
            _endPoint = clientSocket.RemoteEndPoint;
            _connected = true;
            _onServerClient = true;
            clientData = new();
        }

        public bool Connect(IPAddress ip, int port, int maxAttempts = 1, int timeout = 1000)
        {
            _clientSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEndPoint = new(ip, port);
            int attempts = 0;
            while (attempts < maxAttempts)
            {
                try
                {
                    attempts++;
                    Log($"Trying to connect to {serverEndPoint} [{attempts}/{maxAttempts}]", ConsoleColor.Gray);
                    _clientSocket.Connect(serverEndPoint);
                    _endPoint = _clientSocket.RemoteEndPoint;
                    _connected = true;
                    Log($"Connected to {serverEndPoint}", ConsoleColor.Green);
                    OnClientConnected?.Invoke(this);
                    SendPacket(255, clientData.Serialize());
                    StartReceive();
                    return true;
                }
                catch (Exception ex)
                {
                    Log($"Connection failed: {ex.Message}", ConsoleColor.Red);
                    if (attempts < maxAttempts) Thread.Sleep(timeout);
                }
            }
            return false;
        }

        private bool ReceiveExactly(byte[] buffer, int size)
        {
            int totalRead = 0;
            while (totalRead < size)
            {
                try
                {
                    int read = _clientSocket!.Receive(buffer, totalRead, size - totalRead, SocketFlags.None);
                    if (read == 0) return false;
                    totalRead += read;
                }
                catch { return false; }
            }
            return true;
        }

        public void StartReceive()
        {
            if (!_connected || _clientSocket == null) return;
            _clientReceiveThread = new(() =>
            {
                byte[] headerBuffer = new byte[5];
                try
                {
                    while (_connected && _clientSocket != null)
                    {
                        if (!ReceiveExactly(headerBuffer, 5)) break;
                        byte type = headerBuffer[0];
                        int packetLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(headerBuffer, 1));
                        if (packetLength < 0 || packetLength > 10 * 1024 * 1024)
                        {
                            Log("Invalid packet size received", ConsoleColor.Red);
                            break;
                        }
                        byte[]? packetBytes = null;
                        if (packetLength > 0)
                        {
                            packetBytes = new byte[packetLength];
                            if (!ReceiveExactly(packetBytes, packetLength)) break;
                        }
                        HandlePacket(type, packetBytes);
                    }
                }
                catch (Exception ex)
                {
                    Log($"Receive error: {ex.Message}", ConsoleColor.Red);
                }
                finally
                {
                    Disconnect();
                }
            });
            _clientReceiveThread.IsBackground = true;
            _clientReceiveThread.Start();
        }

        private void HandlePacket(byte type, byte[]? data)
        {
            if (type == 255 && _onServerClient && data !=null)
            {
                clientData = new ClientData(data);
                OnClientDataLoaded?.Invoke(this);
            }
            else if (type == 254 && !_onServerClient && data != null)
            {
                _UID = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(data, 0));
                OnClientDataLoaded?.Invoke(this);
            }
            else
            {
                OnPacketReceived?.Invoke(this, type, data);
            }
        }

        public void SendPacket(byte type, byte[]? data = null)
        {
            if (!Connected || _clientSocket == null) return;
            try
            {
                int dataLength = data?.Length ?? 0;
                byte[] packet = new byte[5 + dataLength];
                packet[0] = type;
                byte[] lenBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(dataLength));
                Buffer.BlockCopy(lenBytes, 0, packet, 1, 4);
                if (data != null && dataLength > 0)
                {
                    Buffer.BlockCopy(data, 0, packet, 5, dataLength);
                }
                _clientSocket.Send(packet);
            }
            catch (Exception ex)
            {
                Log($"Send error: {ex.Message}", ConsoleColor.Red);
                Disconnect();
            }
        }

        public void Disconnect()
        {
            if (!_connected) return;
            _connected = false;
            Log($"Disconnected from {_endPoint}", ConsoleColor.Red);
            OnClientDisconnected?.Invoke(this);
            try { _clientSocket?.Shutdown(SocketShutdown.Both); } catch { }
            try { _clientSocket?.Close(); } catch { }
        }

        private void Log(string message, ConsoleColor color)
        {
            if (!ShowLogs) return;
            lock (Console.Out) 
            {
                if (ShowLogsTime)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[{DateTime.Now:HH:mm:ss}");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write($"/Client");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"]");
                }
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }
}