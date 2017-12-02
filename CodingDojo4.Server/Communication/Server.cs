using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CodingDojo4.Server.Communication
{
    class Server
    {

        private TcpListener _Listener { get; set; }

        private bool _stop = false;

        public Server()
        {
            this._Listener = new TcpListener(IPAddress.Any, 1010);

        }

        public void Start()
        {
            this._Listener.Start();
            while (!_stop)
            {
                if (this._Listener.Pending())
                {
                    var client = this._Listener.AcceptTcpClient();
                    Console.WriteLine("New Client connected!");
                }
            }
            this._Listener.Stop();
        }

        public void Stop()
        {
            this._stop = true;
        }

    }
}
