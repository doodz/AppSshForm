using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApptestSsh.Core.DataBase;
using Autofac;
using Doods.LibSsh.Beans;
using Doods.LibSsh.Interfaces;
using Doods.LibSsh.Queries;
using Doods.StdFramework;
using Doods.StdFramework.Interfaces;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Queries.GroupedQueries;

namespace ApptestSsh.Core.View.MainPage
{
    public class MainPageViewModel : BaseViewModel
    {
        private VcgencmdBean _vcgencmdBean;

        public VcgencmdBean VcgencmdBean
        {
            get => _vcgencmdBean;
            set => SetProperty(ref _vcgencmdBean, value);
        }


        
        private SystemBean _systemBean;

        public SystemBean SystemBean
        {
            get => _systemBean;
            set => SetProperty(ref _systemBean, value);
        }
        public MainPageViewModel(ILogger logger) : base(logger)
        {
        }

        protected override async Task Load()
        {
            var ssh = App.Container.Resolve<ISshService>();
            var host = new Host
            {
                HostName = "192.168.1.73",
                UserName = "pi",
                Password = "raspberry"
            };

            ssh.Host = host;
            ssh.Initialise();

            var test = new VcgencmdQuery(ssh);
            _vcgencmdBean =await test.RunAsync(Token);

            SystemBean = await new SystemInfoQueries(ssh).RunAsync(Token);


        }
    }
}
