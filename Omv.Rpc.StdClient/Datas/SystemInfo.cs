using Doods.StdFramework;

namespace Omv.Rpc.StdClient.Datas
{
    public class SystemInfo : ObservableObject
    {

        public string Toto => "MonToto";

        private SystemInfoData _hostName;
        private SystemInfoData _version;
        private SystemInfoData _kernel;
        private SystemInfoData _systemTime;
        private SystemInfoData _uptime;
        private SystemInfoData _loadAverage;
        private SystemInfoData _cpuUsage;
        private SystemInfoData _memoryUsage;
        private SystemInfoData _processor;


        public SystemInfoData HostName
        {
            get => _hostName;
            set => SetProperty(ref _hostName, value);
        }
        public SystemInfoData Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }
        public SystemInfoData Kernel
        {
            get => _kernel;
            set => SetProperty(ref _kernel, value);
        }
        public SystemInfoData SystemTime
        {
            get => _systemTime;
            set => SetProperty(ref _systemTime, value);
        }
        public SystemInfoData Uptime
        {
            get => _uptime;
            set => SetProperty(ref _uptime, value);
        }
        public SystemInfoData LoadAverage
        {
            get => _loadAverage;
            set => SetProperty(ref _loadAverage, value);
        }
        public SystemInfoData CpuUsage
        {
            get => _cpuUsage;
            set => SetProperty(ref _cpuUsage, value);
        }
        public SystemInfoData MemoryUsage
        {
            get => _memoryUsage;
            set => SetProperty(ref _memoryUsage, value);
        }
        public SystemInfoData Processor
        {
            get => _processor;
            set => SetProperty(ref _processor, value);
        }
    }
}