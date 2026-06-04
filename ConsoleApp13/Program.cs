namespace ConsoleApp3;
using MyNetwork;
using System.Net;
using System.Text;
class Program
{
    public static string IP="172.19.68.108";
    public static string PORT="12345";
    static void Main(string[] args)
    {
       var client = new NetworkClient();
       Console.WriteLine("Введите ваше имя :");
       client.clientData.Name = Console.ReadLine()?.Trim() ?? "UnnamedClient";
       client.ShowLogs = true;
       client.OnClientConnected += (client) =>
       {
           Console.WriteLine($"connected: {client.clientData.Name}");
       };
       client.OnClientDisconnected += (client) =>
       {
           Console.WriteLine($"disconnected: {client.clientData.Name}");
       };
       client.OnPacketReceived += (client,type, packet) =>
       {
               MyPacket myPacket = new MyPacket("", "");
               myPacket.decode(packet);
               Console.WriteLine($"{myPacket.name}: {myPacket.message}");
       };
       bool connected = client.Connect(IPAddress.Parse(IP), int.Parse(PORT));
       if(connected)
       {
           Console.WriteLine("подключение к серверу успешно.");
       }
       else
       {
           Console.WriteLine("неудачная попытка подключения к серверу.");
       }
        Console.WriteLine("Введите сообщение для отправки на сервер (или 'exit' для выхода):");
       while(true)
       {
           string message = Console.ReadLine();
           if(message == null)
           {
               break;
           }
           else if(message.ToLower() == "exit")
           {
               break;
           }
           client.SendPacket(1, new MyPacket(client.clientData.Name, message).encode());
    }
}
public class MyPacket
        {
            public string name;
            public string message;

            public MyPacket(string name, string message)
            {
                this.name = name;
                this.message = message;
            }

            public byte[] encode()
            {
                using var ms = new MemoryStream();
                using var writer = new BinaryWriter(ms, Encoding.UTF8);

                writer.Write(name);
                writer.Write(message);

                return ms.ToArray();
            }

            public void decode(byte[] bytes)
            {
                using var ms = new MemoryStream(bytes);
                using var reader = new BinaryReader(ms, Encoding.UTF8);

                name = reader.ReadString();
                message = reader.ReadString();
            }
        }
}