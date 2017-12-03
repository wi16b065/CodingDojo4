using System;
using CodingDojo4.Client.Utility;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace CodingDojo4.Client.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        public Storage Storage { get; set; }

        public RelayCommand ConnectBtnClick { get; set; }
        public RelayCommand SendBtnClick { get; set; }

        private CodingDojo4.Client.Communication.Client Client { get; set; }

        

        public MainViewModel()
        {
            this.Storage = Storage.Current;
            InitCommands();
        }

        private void InitCommands()
        {
            this.ConnectBtnClick = new RelayCommand(this.ConnectToServer, () => !Storage.isConnected);
            this.SendBtnClick = new RelayCommand(this.SendMessage, () => Storage.Message?.Length > 0 && Storage.isConnected);
        }

        private void ConnectToServer()
        {
            this.Client = new Communication.Client();
            this.Client.Start();
            Storage.isConnected= true;
        }

        private void SendMessage()
        {
            this.Client.SendMessage(Storage.Message);
            this.Storage.Message = "";
        }

    }
}