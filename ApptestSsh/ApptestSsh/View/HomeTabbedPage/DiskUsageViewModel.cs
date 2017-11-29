using Doods.StdFramework;
using Doods.StdFramework.Helpers;
using Doods.StdLibSsh.Beans;
using Microcharts;
using SkiaSharp;
using System.Collections.Generic;

namespace ApptestSsh.Core.View.HomeTabbedPage
{
    public class DiskUsageViewModel : ObservableObject
    {
        private string _fileSystem;
        private string _size;
        private string _used;
        private string _available;
        private string _usedPercent;
        private string _mountedOn;

        public string FileSystem
        {
            get => _fileSystem;
            set => SetProperty(ref _fileSystem, value);
        }

        public string Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        public string Used
        {
            get => _used;
            set => SetProperty(ref _used, value);
        }

        public string Available
        {
            get => _available;
            set => SetProperty(ref _available, value);
        }

        public string UsedPercent
        {
            get => _usedPercent;
            set => SetProperty(ref _usedPercent, value);
        }

        public string MountedOn
        {
            get => _mountedOn;
            set => SetProperty(ref _mountedOn, value);
        }


        public IEnumerable<Entry> Entries { get; set; }

        public RadialGaugeChart RadialGaugeChart { get; set; }

        public DiskUsageViewModel(DiskUsageBean disck)
        {
            _fileSystem = disck.FileSystem;
            _size = disck.Size;
            _used = disck.Used;
            _available = disck.Available;
            _usedPercent = disck.UsedPercent;
            _mountedOn = disck.MountedOn;

            NumbersHerpers.TryParseFloat(_size, out var floatSize);
            var entrySize = new Entry(floatSize);
            //entrySize.Label = "Size";
            //entrySize.ValueLabel = _size;

            NumbersHerpers.TryParseFloat(_used, out var floatUsed);


            var entryUse = new Entry(floatUsed);
            //entryUse.Label = "Used";
            //entryUse.ValueLabel = _used;

            NumbersHerpers.TryParseFloat(_usedPercent, out var floatPercent);

            entryUse.Color = SKColors.DarkOrange;
            if (floatPercent > 90)
                entryUse.Color = SKColors.DarkRed;
            if (floatPercent < 60)
                entryUse.Color = SKColors.DarkGreen;

            Entries = new Entry[] { entrySize, entryUse };
            RadialGaugeChart = new RadialGaugeChart() { Entries = Entries };
        }
    }
}