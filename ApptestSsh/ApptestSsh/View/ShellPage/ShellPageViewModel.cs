using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Doods.StdFramework;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Interfaces;
using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Security;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.ShellPage
{
    public class ShellPageViewModel : BaseViewModel
    {

        //enter \r
        //tab \t
        // up \0
        //backspace \b


        private ShellStream _shell;
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        private string _lines;

        public ICommand UpdateCmd { get; }
        public ICommand ClearCmd { get; }
        private string _lasteReceived;

        public string Lines
        {
            get => _lines;
            set => SetProperty(ref _lines, value);
        }

        public ShellPageViewModel(ILogger logger) : base(logger)
        {
            UpdateCmd = new Command(installPiHole);
            ClearCmd = new Command(p => Lines = string.Empty);
        }

        private async void Update()
        {
            await _streamWriter.WriteLineAsync("sudo apt-get update");
            //Lines = _shell.Expect(new Regex(@":.*>#"), new TimeSpan(0, 0, 5));
        }



        private void installPiHole()
        {
            var str = "curl - sSL https://install.pi-hole.net | bash";


            _streamWriter.WriteAsync('\r');

        }

        protected override Task OnInternalAppearing()
        {
            return ExecuteAsync(p => CreateShell());
        }

        private Task CreateShell()
        {
            var ssh = AppContainer.Container.Resolve<ISshService>();

            _shell = ssh.Client.CreateShellStream("toto", 0, 0, 0, 0, 1024);
            _shell.DataReceived += ShellOnDataReceived;

            _streamWriter = new StreamWriter(_shell);
            _streamReader = new StreamReader(_shell);

            _streamWriter.AutoFlush = true;
            //wr.WriteLine("sudo apt-get update");
            return Task.FromResult(0);
        }

        private void ShellOnDataReceived(object sender, ShellDataEventArgs shellDataEventArgs)
        {
            var str = System.Text.Encoding.UTF8.GetString(shellDataEventArgs.Data);
            if (str != _lasteReceived)
            {
                Lines += str;
                _lasteReceived = str;
            }
            //Lines += shellDataEventArgs.Line + Environment.NewLine;
        }

        protected override Task OnInternalDisappearing()
        {
            _shell.DataReceived -= ShellOnDataReceived;
            _shell.Dispose();
             _streamWriter?.Dispose();
            _streamReader?.Dispose();
            _lines = _lasteReceived = null;
            return Task.FromResult(0);
        }

        //~ShellPageViewModel()
        //{

        //    _shell.DataReceived -= ShellOnDataReceived;
        //}
    }
}