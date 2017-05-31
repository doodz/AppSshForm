using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdFramework.Mvvm;
using Renci.SshNet;
using Renci.SshNet.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.ShellPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellPage : ViewPage<ShellPageViewModel>
    {
        public ShellPage()
        {
            InitializeComponent();
        }
    }
}