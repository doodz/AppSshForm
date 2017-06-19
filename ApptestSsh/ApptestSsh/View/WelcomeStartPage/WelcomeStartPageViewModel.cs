using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doods.StdFramework;
using Doods.StdFramework.Interfaces;

namespace ApptestSsh.Core.View.WelcomeStartPage
{
    public class WelcomeStartPageViewModel : BaseViewModel
    {
        private string _WelcomMessage = "Welcome to SshPi";

        public string WelcomMessage
        {
            get => _WelcomMessage;
            set => SetProperty(ref _WelcomMessage, value);
        }

        public WelcomeStartPageViewModel(ILogger logger) : base(logger)
        {
        }
    }
}