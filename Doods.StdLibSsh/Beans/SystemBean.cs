using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doods.StdFramework;

namespace Doods.StdLibSsh.Beans
{
    public class SystemBean : ObservableObject
    {
        private string _distributionName;

        public string DistributionName
        {
            get => _distributionName;
            set => SetProperty(ref _distributionName, value);
        }

        private OsMemoryBean _osMemory;

        public OsMemoryBean OsMemory
        {
            get => _osMemory;
            set => SetProperty(ref _osMemory, value);
        }

        private string _cpuSerial;
        public string CpuSerial
        {
            get => _cpuSerial;
            set => SetProperty(ref _cpuSerial, value);
        }

        private double _averageLoad;
        public double AverageLoad
        {
            get => _averageLoad;
            set => SetProperty(ref _averageLoad, value);
        }

        private double _uptime;
        public double Uptime
        {
            get => _uptime;
            set => SetProperty(ref _uptime, value);
        }
    }
}