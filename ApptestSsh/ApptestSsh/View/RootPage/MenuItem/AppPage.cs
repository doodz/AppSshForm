using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApptestSsh.Core.View.RootPage.MenuItem
{
    public class DeepLinkPage
    {
        public AppPage Page { get; set; }
        public string Id { get; set; }
    }
    public enum AppPage
    {
        Login =0,
        HostManager,
        Home,
        Shell,
        Settings
    }
}
