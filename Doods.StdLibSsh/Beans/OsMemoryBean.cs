using Doods.LibSsh.Enums;
using Doods.StdFramework;

namespace Doods.LibSsh.Beans
{
    public class OsMemoryBean : ObservableObject
    {
        private MemoryBean _totalMemory;
        private MemoryBean _totalUsed;
        private MemoryBean _totalFree;
        private float _percentageUsed;
        private string _errorMessage;

        public MemoryBean TotalMemory
        {
            get => _totalMemory;
            set => SetProperty(ref _totalMemory, value);
        }

        public MemoryBean TotalUsed
        {
            get => _totalUsed;
            set => SetProperty(ref _totalUsed, value);
        }

        public MemoryBean TotalFree
        {
            get => _totalFree;
            set => SetProperty(ref _totalFree, value);
        }

        public float PercentageUsed
        {
            get => _percentageUsed;
            set => SetProperty(ref _percentageUsed, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public OsMemoryBean(long totalMemory, long totalUsed)
        {
            _totalMemory = MemoryBean.From(Memory.KB, totalMemory);
            _totalUsed = MemoryBean.From(Memory.KB, totalUsed);
            _totalFree = MemoryBean.From(Memory.KB, totalMemory - totalUsed);
            _percentageUsed = (float) totalUsed / (float) totalMemory;
        }

        public OsMemoryBean(string str)
        {
            _errorMessage = str;
        }
    }
}