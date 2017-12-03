using System.Net.Sockets;

namespace CodingDojo4.Server.Model
{
    public class User
    {
        public string Name { get; set; }
        public TcpClient Client { get; set; }

        public User(string name)
        {
            this.Name = name;
        }

        public User(string name, TcpClient client)
        {
            Name = name;
            Client = client;
        }

    }
}