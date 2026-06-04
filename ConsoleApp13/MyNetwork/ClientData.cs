
using System.Text;

namespace MyNetwork
{
    public class ClientData
    {
        public string Name = "";

        public ClientData() { }

        public ClientData(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            using (var reader = new BinaryReader(ms, Encoding.UTF8))
            {
                Name = reader.ReadString(); 
            }
        }

        public byte[] Serialize()
        {
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms, Encoding.UTF8))
            {
                writer.Write(Name);      
                return ms.ToArray();
            }
        }
    }
}
