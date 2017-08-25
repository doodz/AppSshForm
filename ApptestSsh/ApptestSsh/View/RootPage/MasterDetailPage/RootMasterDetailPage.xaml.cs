using Doods.StdFramework.Mvvm;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.RootPage.MasterDetailPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootMasterDetailPage : BaseMasterDetailPage
    {
        public RootMasterDetailPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

            var res = MasterPage.ListView.ItemsSource.Cast<RootMasterDetailPageMenuItem>().FirstOrDefault();

            var select = MasterPage.ListView.SelectedItem;
            SetPage(res);

        }


        private void SetPage(RootMasterDetailPageMenuItem item)
        {
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            //Detail = new DoodsNavigationPage(page);
            Detail = page;
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as RootMasterDetailPageMenuItem;
            SetPage(item);
        }

        private DateTime _date;
        protected override bool OnBackButtonPressed()
        {
            if (IsPresented)
                return base.OnBackButtonPressed();

            if (_date < DateTime.Now.AddSeconds(-3))
            {
                //App.Container.Resolve<INotificator>().Toast("Cliquer une nouvelle fois pour fermer l'application");
                _date = DateTime.Now;
                return true;
            }
            return true;
        }
    }
}