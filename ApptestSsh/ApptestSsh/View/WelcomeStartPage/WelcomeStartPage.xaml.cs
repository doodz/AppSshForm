using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.StdFramework.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.WelcomeStartPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomeStartPage : ViewPage<WelcomeStartPageViewModel>
    {
        public WelcomeStartPage()
        {
            InitializeComponent();
            
        }
    }

   
}