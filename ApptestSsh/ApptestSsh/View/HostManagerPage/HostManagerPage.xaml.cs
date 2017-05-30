using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ApptestSsh.Core.DataBase;
using Doods.StdFramework.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.HostManagerPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HostManagerPage : ViewPage<HostManagerPageViewModel>
    {
        public HostManagerPage()
        {
            InitializeComponent();
           
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.DisplayAction();
        }
           

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //if (e.SelectedItem == null)
            //    return;

            //if (sender is Host h)
            //{
            //    await DisplayAlert("Selected",h.HostName, "OK");
            //}

            ////Deselect Item
            //((ListView)sender).SelectedItem = null;
        }
    }

}