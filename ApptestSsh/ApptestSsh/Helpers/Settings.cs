using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doods.StdFramework;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ApptestSsh.Core.Helpers
{
    public class Settings : ObservableObject
    {
        private const string FirstRunKey = "first_run";

        private static readonly Lazy<Settings> _settings = new Lazy<Settings>(() => new Settings());

        /// <summary>
        /// Gets or sets the current settings. This should always be used
        /// </summary>
        /// <value>The current.</value>
        public static Settings Current => _settings.Value;


        private static ISettings AppSettings => CrossSettings.Current;

        /// <summary>
        /// Gets or sets a value indicating first run
        /// </summary>
        /// <value><c>true</c> if  first run; otherwise, <c>false</c>.</value>
        public bool FirstRun
        {
            get => AppSettings.GetValueOrDefault(FirstRunKey, true);
            set
            {
                if (AppSettings.AddOrUpdateValue(FirstRunKey, value))
                    SetPropertyChanged(nameof(FirstRun));
            }
        }


        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        private  long _LastHostId;
        public  long LastHostId
        {
            get => AppSettings.GetValueOrDefault(nameof(LastHostId), -1L);
            set
            {
                if (AppSettings.AddOrUpdateValue(nameof(LastHostId), value))
                    SetPropertyChanged(nameof(LastHostId));
            }
        }
    }
}