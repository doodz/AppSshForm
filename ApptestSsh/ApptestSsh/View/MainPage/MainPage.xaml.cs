using System;
using System.IO;
using System.Threading.Tasks;
using ApptestSsh.Core.Services;
using ApptestSsh.Core.View.Login;
using ApptestSsh.Core.View.MainPage;
using Autofac;
using Doods.StdFramework.Mvvm;
using Doods.StdLibSsh;
using Doods.StdLibSsh.Queries.GroupedQueries;
using Renci.SshNet;
using Xamarin.Forms;

namespace ApptestSsh.Core
{
    public partial class MainPage : ViewPage<MainPageViewModel>
    {
        
        public MainPage()
        {
            InitializeComponent();           
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {

            await NavigationService.GoToHomeTabbed();
        }

        public ConnectionInfo CreateConnectionInfo()
        {
            const string privateKeyFilePath = @"C:\some\private\key.pem";
            ConnectionInfo connectionInfo;
            using (var stream = new FileStream(privateKeyFilePath, FileMode.Open, FileAccess.Read))
            {
                var privateKeyFile = new PrivateKeyFile(stream);
                AuthenticationMethod authenticationMethod =
                    new PrivateKeyAuthenticationMethod("ubuntu", privateKeyFile);

                connectionInfo = new ConnectionInfo(
                    "my.server.com",
                    "ubuntu",
                    authenticationMethod);
            }

            return connectionInfo;
        }

        private void rest()
        {
            // Setup Credentials and Server Information

            // Execute a (SHELL) Command - prepare upload directory
            using (var sshclient = new SshClient("192.168.1.73", "pi", "raspberry"))
            {
                sshclient.Connect();
                using (var cmd = sshclient.CreateCommand("/usr/bin/which vcgencmd"))
                {
                    cmd.Execute();
                    DisplayAlert("Alert", "Command>" + cmd.CommandText + Environment.NewLine + "Return Value = " + cmd.ExitStatus + Environment.NewLine + cmd.Result, "OK");
                    Console.WriteLine("Command>" + cmd.CommandText);
                    Console.WriteLine("Return Value = {0}", cmd.ExitStatus);
                }


                sshclient.Disconnect();
            }

        }

    }
}
