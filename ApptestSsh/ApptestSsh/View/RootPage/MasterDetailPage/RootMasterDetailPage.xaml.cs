using System;
using Doods.StdFramework.Mvvm;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.RootPage.MasterDetailPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootMasterDetailPage : BaseMasterDetailPage<RootMasterDetailPageMenuItem>
    {
        public RootMasterDetailPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

            var res = MasterPage.ListView.ItemsSource.Cast<RootMasterDetailPageMenuItem>().FirstOrDefault();

            //var select = MasterPage.ListView.SelectedItem;
            SetPage(res);

        }

        protected override void OnSetPage()
        {
            MasterPage.ListView.SelectedItem = null;
        }


        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as RootMasterDetailPageMenuItem;
            SetPage(item);
        }

        protected void SetPage(RootMasterDetailPageMenuItem item)
        {
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            //Detail = new DoodsNavigationPage(page);
            Detail = page;
            IsPresented = false;

            OnSetPage();
        }
    }
}