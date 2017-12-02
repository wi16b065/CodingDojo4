using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CodingDojo4.Server.Communication
{
    public class Listener
    {

        private TcpListener _Listener { get; set; }

        private bool _stop = false;

        public Listener()
        {
            this._Listener = new TcpListener(IPAddress.Loopback, 10100);

        }

        public void Start()
        {
            this._Listener.Start();
            Console.WriteLine("Start Listening...");
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
            Console.WriteLine("Server stopped listening.");
        }

    }
}
