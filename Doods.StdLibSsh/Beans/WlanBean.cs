using Doods.StdFramework;

namespace Doods.StdLibSsh.Beans
{
    public class WlanBean : ObservableObject
    {
        private int _linkQuality;

        public int LinkQuality
        {
            get => _linkQuality;
            set => SetProperty(ref _linkQuality, value);
        }

        private int _signalLevel;
        public int SignalLevel
        {
            get => _signalLevel;
            set => SetProperty(ref _signalLevel, value);
        }
    }
}
