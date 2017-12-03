using CodingDojo4.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodingDojo4.Server.Communication
{
    public class Listener
    {

        private TcpListener TcpListener { get; set; }

        private bool _stop = false;

        List<ClientHandler> clients = new List<ClientHandler>();


        public Listener()
        {
            this.TcpListener = new TcpListener(IPAddress.Loopback, 10100);
        }

        public void Start()
        {
            this.TcpListener.Start();
            Console.WriteLine("Start Listening...");

            while (!_stop)
            {
                if (this.TcpListener.Pending())
                {
                    var client = this.TcpListener.AcceptTcpClient();
                    Console.WriteLine("New Client connected!");
                    //new Thread for new client
                    var clientThread = new Thread(() =>
                    {
                        //create new Clienthandler in new Thread to receive messages
                        var clientHandler = new ClientHandler(this, client);
                        //start receiving messages 
                        clientHandler.StartReceiving();
                    });

                    clientThread.Start();
                }
            }
            this.TcpListener.Stop();
        }

        public void Stop()
        {
            this._stop = true;
            Console.WriteLine("Server stopped listening.");
        }

    }
}
