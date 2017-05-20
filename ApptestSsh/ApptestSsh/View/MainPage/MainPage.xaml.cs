using System;
using System.IO;
using ApptestSsh.Core.View.MainPage;
using Autofac;
using Doods.StdLibSsh;
using Doods.StdLibSsh.Queries.GroupedQueries;
using Renci.SshNet;
using Xamarin.Forms;

namespace ApptestSsh.Core
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel vm;
        public MainPage()
        {
            InitializeComponent();
            vm = App.Container.Resolve<MainPageViewModel>();
            BindingContext = vm;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            //rest();
            //return;
            var client = new ClientSsh();
            var str = client.GetServeurVersion();

            var test = new VcgencmdQuery(client);
            var res = test.Run();


            MyLabel.Text = str;

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
            using (var sshclient = new SshClient("192.168.1.50", "root", "nostrqdq;us"))
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


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await vm.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.OnDisappearing();
        }
    }
}
