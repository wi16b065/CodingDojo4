using CodingDojo4.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingDojo4.Server.Models
{
    public class Message
    {
        public User Sender { get; set; }

        public string TextMessage { get; set; }
        public Message(User sender, string textMessage)
        {
            Sender = sender;
            TextMessage = textMessage;
        }

        public string DisplayText
        {
            get { return $"{Sender.Name}: {TextMessage}"; }
        }
    }
}
