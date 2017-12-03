using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;


namespace CodingDojo4.Client.Utility
{
    public class Storage : ViewModelBase
    {
        private static Storage _current = null;
        private string _message = null;
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
        public string Username { get; set; }

        public bool isConnected { get; set; } = false;

        public string Message
        {
            get { return _message; }
            set { this._message = value; RaisePropertyChanged("Message"); }
        }


        public ObservableCollection<string> Messages { get; set; }

        private Storage()
        {
            this.Messages = new ObservableCollection<string>();
        }
    }
}
