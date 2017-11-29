using System;
using Xamarin.Forms;

namespace Doods.StdFramework.Mvvm
{
    public interface IPageMenuItem
    {
        int Id { get; }
        string Title { get; }

        Type TargetType { get; }

        string Icon { get; }

    }

    public class BaseMasterDetailPage<T> : MasterDetailPage where T : IPageMenuItem
    {

        protected void SetPage(T item)
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

        protected virtual void OnSetPage()
        {

        }
        protected DateTime _date;
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
