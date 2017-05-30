using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApptestSsh.Core.View.MainPage;
using Doods.StdFramework.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.HomeTabbedPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeTabbedPage : BaseTabbedPage<HomeTabbedPageViewModel>
    {
        public HomeTabbedPage()
        {
            InitializeComponent();
        }
    }
}