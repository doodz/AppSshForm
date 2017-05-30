using System;
using System.Windows.Input;
using ApptestSsh.Core.DataBase;
using ApptestSsh.Core.Services;
using Doods.StdFramework;
using Doods.StdFramework.Interfaces;
using Doods.StdRepository.Base;
using Renci.SshNet;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.LoginPage
{
    public class LoginViewModel : BaseViewModel
    {
        //private ConnectionInfo _connectionInfo;

        private Host _hostObj;

        public Host HostObj
        {
            get { return _hostObj; }
            set { SetProperty(ref _hostObj,value);
                InitDisplay();
            }
        }

        private void InitDisplay()
        {
            if (_hostObj == null) return;

             _host = _hostObj.HostName;
             _port = _hostObj.Port;
             _username = _hostObj.UserName;
             _password = _hostObj.Password;
        }

        private string _host;
        private bool _canSave;
        private string _password;

        private int _port = 22;

        private string _username;

        private readonly IRepository _repository;

        public LoginViewModel(ILogger logger, IRepository reposotiry) : base(logger)
        {
            _hostObj = new Host();
            SaveCmd = new Command(p => SaveHost(), p => CanSave());
            TestCmd = new Command(p => TestLogin());
            _repository = reposotiry;
        }

        private bool CanSave()
        {
            return _canSave;
        }

        public Command SaveCmd { get; }
        public ICommand TestCmd { get; }

        public int Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

        public string Host
        {
            get => _host;
            set => SetProperty(ref _host, value);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool TestLogin()
        {
            _canSave = false;

            _hostObj.HostName = _host;
            _hostObj.Port = _port;
            _hostObj.UserName = _username;
            _hostObj.Password = _password;

            try
            {
                var test =
                    new PasswordConnectionInfo(_host, _port, _username, _password) {Timeout = TimeSpan.FromSeconds(3)};

                using (var client = new SshClient(test))
                {
                    client.Connect();
                    _canSave = client.IsConnected;
                    Application.Current.MainPage.DisplayAlert("SSH", $"Connected to {_host}", "OK");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Application.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "OK");
            }

            SaveCmd.ChangeCanExecute();
            return _canSave;
        }

        private async void SaveHost()
        {
            await _repository.InsertAsync(_hostObj);
            await NavigationService.GoBack();
        }
    }
}