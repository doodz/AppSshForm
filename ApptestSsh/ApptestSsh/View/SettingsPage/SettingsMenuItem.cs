using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ApptestSsh.Core.View.RootPage.MenuItem;
using Doods.StdFramework;

namespace ApptestSsh.Core.View.Settings
{
    public class SettingsMenuItem : ObservableObject
    {
        private string name;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string subtitle;

        public string Subtitle
        {
            get => subtitle;
            set => SetProperty(ref subtitle, value);
        }

        public string Icon { get; set; }
        public string Parameter { get; set; }

        public AppPage Page { get; set; }
        public ICommand Command { get; set; }
    }
}