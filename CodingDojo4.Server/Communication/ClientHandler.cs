using CodingDojo4.Server.Model;
using CodingDojo4.Server.Models;
using CodingDojo4.Server.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodingDojo4.Server.Communication
{
    public class ClientHandler
    {

        private Listener Listener { get; set; }
        private TcpClient Client { get; set; }
        private User User { get; set; }

        public ClientHandler(Listener listener, TcpClient client)
        {
            Listener = listener;
            Client = client;
        }

        public void StartReceiving()
        {
            while (true)
            {
                try
                {
                    if (Client.Available <= 0) continue;
                    var stream = this.Client.GetStream();
                    var streamReader = new StreamReader(stream);


                    string messageLine = null;

                    while ((messageLine = streamReader.ReadLine()) != null)
                    {
                        HandleIncomingMessage(messageLine);
                    }
                }
                catch (IOException)
                {
                    break; ;
                }

            }
        }

        private void HandleIncomingMessage(string message)
        {
            Console.WriteLine(message);
            if (message.Contains("username:"))
            {
                var username = message.Split(':')[1];
                this.User = new User(username, this.Client);
                Storage.Current.Clients.Add(this.User);
                //Update GUI
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Storage.Current.ConnectedUsers.Add(this.User);
                });
            }
            else if (message.Contains("message:"))
            {
                var textMessage = message.Split(':')[1];
                //Update GUI
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Storage.Current.ReceivedMessages.Add(new Message(this.User, textMessage));
                });
            }
            else if (message == "@quit")
            {
                this.Disconnect();
            }
        }

        private void Disconnect()
        {
            //Update GUI
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (this.User != null)
                {
                    Storage.Current.ConnectedUsers.Remove(this.User);
                    this.User.Client?.Close();
                }
            });
        }
    }
}
