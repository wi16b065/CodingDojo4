using CodingDojo4.Client.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CodingDojo4.Client.Communication
{
    public class Client
    {
        private TcpClient TcpClient { get; set; }

        private NetworkStream networkStream = null;

        private Thread incomeMessageThread = null;

        private bool shouldStop = false;

        public Client()
        {
            this.TcpClient = new TcpClient();
        }

        public void Start()
        {
            this.TcpClient.Connect(IPAddress.Loopback, 10100);

            networkStream = this.TcpClient.GetStream();
            var streamWriter = new StreamWriter(networkStream);
            streamWriter.WriteLine($"username:{Storage.Current.Username}");
            streamWriter.Flush();

            incomeMessageThread = new Thread(() =>
            {
                try
                {
                    while (!shouldStop)
                    {
                        if (this.TcpClient.Available <= 0) continue;

                        networkStream = this.TcpClient.GetStream();
                        var streamReader = new StreamReader(networkStream);

                        string messages = null;

                        while ((messages = streamReader.ReadLine()) != null)
                        {
                            HandleIncomingMessage(messages);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });

            incomeMessageThread.Start();
        }

        public void SendMessage(string message)
        {
            //write message to stream
            try
            {
                networkStream = this.TcpClient.GetStream();
                var streamWriter = new StreamWriter(networkStream);
                if (message == "@quit")
                {
                    streamWriter.WriteLine(message);
                }
                else
                {
                    streamWriter.WriteLine($"message:{message}");
                }
                streamWriter.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void HandleIncomingMessage(string messages)
        {
            if (messages == null) return;

            var username = messages.Split(':')[0];
            var text = messages.Split(':')[1];

            if (Storage.Current.Username == username)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Storage.Current.Messages.Add($"YOU: {text}");
                });
            }
            else if(username == "server" && text == "@quit")
            {
                Storage.Current.isConnected = false;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Storage.Current.Messages.Add($"{username}: Dropped from server!");
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Storage.Current.Messages.Add($"{username}: {text}");
                });
            }
        }

        //public void Stop()
        //{
        //    this.shouldStop = true;
        //    this.TcpClient.Close();
        //}
    }
}
