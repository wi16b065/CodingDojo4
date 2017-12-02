using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using CodingDojo4.Server.Communication;
using System.Threading;

namespace CodingDojo4.Server.ViewModel
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
        public RelayCommand StartBtnClick { get; set; }
        public RelayCommand StopBtnCLick { get; set; }
        public RelayCommand DropBtnClick { get; set; }

        private bool IsStarted { get; set; } = false;

        //private bool _isStarted = false;

        //private bool IsStarted
        //{
        //    get { return _isStarted; }
        //    set { _isStarted = value; StartBtnClick.RaiseCanExecuteChanged(); StopBtnCLick.RaiseCanExecuteChanged(); }
        //}
        // manual version -> using GalaSoft.MvvmLight.CommandWpf; instead of using GalaSoft.MvvmLight.Command; will raise CanExecuteChanged automatically

        private Listener _listener;

        public MainViewModel()
        {
            initCommands();
        }

        public void initCommands()
        {
            //action = start server...should only be available if server is not already started => isStarted = false
            this.StartBtnClick = new RelayCommand(this.StartServer,
                () => !this.IsStarted);
            this.StopBtnCLick = new RelayCommand(this.StopServer,
                () => this.IsStarted);
            //TODO
            //this.DropBtnClick = new RelayCommand(this.DropClient);

        }

        private void StartServer()
        {
            Thread listenerThread = new Thread(TcpListenerThread);
            listenerThread.Start();
            this.IsStarted = true;
        }

        private void TcpListenerThread()
        {
            this._listener = new Listener();
            this._listener.Start();
        }

        private void StopServer()
        {
            this._listener.Stop();
            this.IsStarted = false;
        }

        private void DropClient()
        {
            throw new NotImplementedException();
        }



    }
}