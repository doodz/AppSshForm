using System;
using System.ComponentModel;
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


        public RubanCmdViewModel RubanCmdViewModel { get; private set; }
        public ICommand UpdateCmd { get; }
        public ICommand ClearCmd { get; }
        public ICommand InstallPiHoleCmd { get; }
        private string _lasteReceived;

        public string Textbash { get; set; }

        public string Lines
        {
            get => _lines;
            set => SetProperty(ref _lines, value);
        }

        public ShellPageViewModel(ILogger logger) : base(logger)
        {
            UpdateCmd = new Command(Update);
            ClearCmd = new Command(p => Lines = string.Empty);
            InstallPiHoleCmd = new Command(p=> InstallPiHole());

            RubanCmdViewModel = new RubanCmdViewModel();
            RubanCmdViewModel.PropertyChanged += RubanCmdViewModelOnPropertyChanged;

        }

        private void RubanCmdViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "CmdStr")
            {
                //Textbash += RubanCmdViewModel.CmdStr;
                _shell.Write(RubanCmdViewModel.CmdStr);
            }
        }

        private async void Update()
        {
            _shell.WriteLine(Textbash);
            //if (Textbash != null)
            //    await _streamWriter.WriteAsync(Textbash);
            //await _streamWriter.WriteLineAsync(Textbash);
            //await _streamWriter.WriteLineAsync("sudo apt-get update");
            //Lines = _shell.Expect(new Regex(@":.*>#"), new TimeSpan(0, 0, 5));
        }


        private void InstallPiHole()
        {
            var str = "curl -sSL https://install.pi-hole.net | bash";
            _streamWriter.WriteLineAsync(str);
        }

        protected override Task OnInternalAppearing()
        {
            return ExecuteAsync(p => CreateShell(),true);
        }

        private Task CreateShell()
        {
            var ssh = AppContainer.Container.Resolve<ISshService>();

            _shell = ssh.Client.CreateShellStream("toto", 0, 0, 0, 0, 1024);
            _shell.DataReceived += ShellOnDataReceived;

            _streamWriter = new StreamWriter(_shell);
            _streamReader = new StreamReader(_shell);

            _streamWriter.AutoFlush = false;
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
            RubanCmdViewModel.PropertyChanged -= RubanCmdViewModelOnPropertyChanged;

            if (_shell != null)
            {
                _shell.DataReceived -= ShellOnDataReceived;
                _shell.Dispose();
            }
            //_streamWriter?.Dispose();
            //_streamReader?.Dispose();
            _lines = _lasteReceived = null;
            return Task.FromResult(0);

        }

        //~ShellPageViewModel()
        //{

        //    _shell.DataReceived -= ShellOnDataReceived;
        //}
    }
}