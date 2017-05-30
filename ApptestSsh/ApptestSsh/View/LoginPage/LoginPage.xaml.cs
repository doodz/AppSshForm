using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApptestSsh.Core.DataBase;
using Autofac;
using Doods.StdFramework.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.Login
{

    public partial class LoginPage
    {
       
        public LoginPage()
        {
           
            InitializeComponent();
        }

        public LoginPage(Host host )
        {
            ViewModel.HostObj = host;
            InitializeComponent();
        }
    }
}