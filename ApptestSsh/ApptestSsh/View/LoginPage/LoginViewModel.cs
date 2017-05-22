using System;
using System.Windows.Input;
using Doods.StdFramework;
using Doods.StdFramework.Interfaces;
using Renci.SshNet;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.Login
{
    public class LoginViewModel : BaseViewModel
    {
        //private ConnectionInfo _connectionInfo;

        private string _host;

        private string _password;

        private int _port;


        private string _username;

        public LoginViewModel(ILogger logger) : base(logger)
        {
            Title = "Login";
            Logger.Info($"{Title} : opened.");
            SaveCmd = new Command(p => TestLogin());
        }

        public ICommand SaveCmd { get; }

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

        private int __i = 2;
        public bool TestLogin()
        {
            bool ok = false;
            try
            {
                var test = new PasswordConnectionInfo(_host, _port, _username, _password);
                test.Timeout = TimeSpan.FromSeconds(__i++);


                //_connectionInfo = new ConnectionInfo(_host, _port, _username,
                //    new PasswordAuthenticationMethod(_username, _password));
                using (var client = new SshClient(test))
                {
                   
                    client.Connect();
                    ok = client.IsConnected;
                }
            }
            catch (Exception ex)
            {
                
                Logger.Error(ex);
            }
            return ok;
        }
    }
}