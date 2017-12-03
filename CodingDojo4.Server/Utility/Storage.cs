using CodingDojo4.Server.Model;
using CodingDojo4.Server.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingDojo4.Server.Utility
{
    public class Storage
    {
        //Singleton pattern
        private static Storage _current = null;

        public static Storage Current
        {
            get
            {
                if (Storage._current == null)
                {
                    Storage._current = new Storage();
                }

                return _current;
            }
        }

        public ObservableCollection<User> ConnectedUsers { get; set; }
        public ObservableCollection<Message> ReceivedMessages { get; set; }
        public List<User> Clients { get; set; }

        public User UserToDrop { get; set; }

        private Storage()
        {
            this.ConnectedUsers = new ObservableCollection<User>();
            this.Clients = new List<User>();
        }

    }
}
