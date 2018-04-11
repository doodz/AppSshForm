using ApptestSsh.Core.View.RootPage.MenuItem;
using Doods.StdFramework;
using System.Windows.Input;

namespace ApptestSsh.Core.View.AboutPage
{
    public class AboutMenuItem : ObservableObject
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _subtitle;

        public string Subtitle
        {
            get => _subtitle;
            set => SetProperty(ref _subtitle, value);
        }

        public string Icon { get; set; }
        public string Parameter { get; set; }

        public AppPage Page { get; set; }
        public ICommand Command { get; set; }
    }
}