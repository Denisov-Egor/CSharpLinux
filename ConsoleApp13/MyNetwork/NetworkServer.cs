using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyNetwork
{
    public delegate void ClientHandler(NetworkClient client);
    public delegate void PacketHandler(NetworkClient client, byte packetType, byte[]? packetData);

    public enum PacketTarget
    {
        SpecificClient,
        AllExceptSpecific,
        AllClients
    }

    public class NetworkServer
    {
        #region Public
        public bool ServerRunning
        {
            get { return _serverRunning; }
        }

        public IEnumerable<NetworkClient> Clients => _clients.Values;

        public bool ShowLogs = false;

        public bool ShowLogsTime = false;

        public event ClientHandler? OnClientConnected;

        public event ClientHandler? OnClientDisconnected;

        public event ClientHandler? OnDataLoaded;

        public event PacketHandler? OnPacketReceived;
        #endregion

        #region Private
        private Socket? _serverSocket;

        private IPEndPoint? _serverEndPoint;

        private Thread? _serverConnectThread;

        private bool _serverRunning;

        private ConcurrentDictionary<int, NetworkClient> _clients = new();
       
        private int _currentCLientUID = 0;
        #endregion

        public bool Start(IPAddress address, int port, int maxClients = 10)
        {
            try
            {
                _serverEndPoint = new(address, port);
                _serverSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Bind(_serverEndPoint);
                _serverSocket.Listen(20);
                _serverRunning = true;
                Log($"Server started on {_serverEndPoint} (Max clients: {maxClients})", ConsoleColor.Green);
                _serverConnectThread = new(() =>
                {
                    while (_serverRunning)
                    {
                        try
                        {
                            Socket incomingSocket = _serverSocket.Accept();
                            if (_clients.Count >= maxClients)
                            {
                                Log($"Rejected {incomingSocket.RemoteEndPoint}: Server full ({maxClients})", ConsoleColor.Yellow);
                                try
                                {
                                    incomingSocket.Shutdown(SocketShutdown.Both);
                                    incomingSocket.Close();
                                }
                                catch {  }
                                continue; 
                            }
                            NetworkClient client = new(_currentCLientUID, incomingSocket);

                            client.OnClientDisconnected += HandleClientDisconnected;
                            client.OnPacketReceived += HandlePacketReceived;
                            client.OnClientDataLoaded += HandleDataLoaded;
                            client.ShowLogs = this.ShowLogs;
                            client.ShowLogsTime = this.ShowLogsTime;

                            if (_clients.TryAdd(_currentCLientUID, client))
                            {
                                client.StartReceive();
                                byte[] uidBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(_currentCLientUID));
                                client.SendPacket(254, uidBytes);
                                HandleClientConnected(client);
                                Interlocked.Increment(ref _currentCLientUID);
                            }
                            else
                            {
                                client.Disconnect();
                            }
                        }
                        catch (Exception ex)
                        {
                            if (_serverRunning)
                                Log($"Accept error: {ex.Message}", ConsoleColor.Red);
                        }
                    }
                });
                _serverConnectThread.IsBackground = true;
                _serverConnectThread.Start();
                return true;
            }
            catch (Exception ex)
            {
                Log($"Critical start error: {ex.Message}", ConsoleColor.DarkRed);
                return false;
            }
        }

        public void HandleClientConnected(NetworkClient client)
        {
            Log($"{client.IP}:{client.Port} connected", ConsoleColor.Gray, ConsoleColor.Yellow, $"{client.IP}:{client.Port}");
            try
            {
                OnClientConnected?.Invoke(client);
            }
            catch (Exception ex)
            {
                Log($"Error in OnClientConnected event: {ex.Message}", ConsoleColor.Red);
            }
        }

        public void HandleClientDisconnected(NetworkClient client)
        {
            string clientName = client.clientData?.Name ?? "Unknown";
            Log($"{client.IP}:{client.Port} ({clientName}) disconnected", ConsoleColor.Gray, ConsoleColor.Red, "disconnected");
            try
            {
                OnClientDisconnected?.Invoke(client);
            }
            catch (Exception ex)
            {
                Log($"Error in OnClientDisconnected event: {ex.Message}", ConsoleColor.Red);
            }
            _clients.TryRemove(client.UID, out _);
        }

        public void HandlePacketReceived(NetworkClient client, byte type, byte[]? packet)
        {
            try
            {
                OnPacketReceived?.Invoke(client, type, packet);
            }
            catch (Exception ex)
            {
                Log($"Error in OnPacketReceived (Type: {type}) from {client.IP}: {ex.Message}", ConsoleColor.Red);
            }
        }

        public void HandleDataLoaded(NetworkClient client)
        {
            if (string.IsNullOrEmpty(client.clientData.Name))
            {
                client.clientData.Name = $"{client.IP}:{client.Port}";
            }
            Log($"{client.IP}:{client.Port} ({client.clientData.Name}) data loaded", ConsoleColor.Gray, ConsoleColor.Cyan, client.clientData.Name);
            try
            {
                OnDataLoaded?.Invoke(client);
            }
            catch (Exception ex)
            {
                Log($"Error in OnDataLoaded event: {ex.Message}", ConsoleColor.Red);
            }
        }

        public void SendPacket(PacketTarget target, byte type, byte[]? packet = null, int clientUID = -1)
        {
            if (_clients == null || _clients.IsEmpty) return;
            switch (target)
            {
                case PacketTarget.SpecificClient:
                    if (clientUID == -1) return;
                    if (_clients.TryGetValue(clientUID, out NetworkClient? client))
                    {
                        client.SendPacket(type, packet); 
                    }
                    break;
                case PacketTarget.AllExceptSpecific:
                    if (clientUID == -1) return;
                    foreach (var pair in _clients)
                    {
                        if (pair.Key != clientUID)
                        {
                            pair.Value.SendPacket(type, packet);
                        }
                    }
                    break;
                case PacketTarget.AllClients:
                    foreach (var pair in _clients)
                    {
                        pair.Value.SendPacket(type, packet);
                    }
                    break;
            }
        }

        public void Stop()
        {
            if (!_serverRunning) return;
            _serverRunning = false;
            Log($"Stopping server on {_serverEndPoint}...", ConsoleColor.Red);
            if (_clients != null)
            {
                foreach (var client in _clients.Values)
                {
                    try { client.Disconnect(); } catch { }
                }
                _clients.Clear();
            }
            try
            {
                _serverSocket?.Close();
            }
            catch (Exception ex)
            {
                Log($"Error closing server socket: {ex.Message}", ConsoleColor.Red);
            }
            if (_serverConnectThread != null && _serverConnectThread.IsAlive)
            {
                _serverConnectThread.Join(1000);
            }
            Log("Server stopped", ConsoleColor.DarkRed);
        }

        private void Log(string message, ConsoleColor color = ConsoleColor.Gray, ConsoleColor? highlightColor = null, string? highlightText = null)
        {
            if (!ShowLogs) return;
            lock (Console.Out)
            {
                if (ShowLogsTime)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[{DateTime.Now:HH:mm:ss}");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write($"/Server");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"]");
                }
                Console.ForegroundColor = color;
                if (highlightText != null && highlightColor != null)
                {
                    int index = message.IndexOf(highlightText);
                    if (index != -1)
                    {
                        Console.Write(message.Substring(0, index));
                        Console.ForegroundColor = highlightColor.Value;
                        Console.Write(highlightText);
                        Console.ForegroundColor = color;
                        Console.WriteLine(message.Substring(index + highlightText.Length));
                    }
                    else Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine(message);
                }
                Console.ResetColor();
            }
        }
    }
}
