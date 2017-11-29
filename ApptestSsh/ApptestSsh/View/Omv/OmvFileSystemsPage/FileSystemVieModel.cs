using Doods.StdFramework.Helpers;
using Microcharts;
using Omv.Rpc.StdClient.Datas;
using SkiaSharp;
using System.Collections.Generic;

namespace ApptestSsh.Core.View.Omv.OmvFileSystemsPage
{
    public class FileSystemVieModel : FileSystem
    {

        public IEnumerable<Entry> Entries { get; private set; }

        public RadialGaugeChart RadialGaugeChart { get; private set; }


        internal void MakeGraph()
        {
            NumbersHerpers.TryParseFloat(Size, out var floatSize);
            var entrySize = new Entry(floatSize);

            NumbersHerpers.TryParseFloat(Available, out var floatAvailable);
            var floatUsed = floatSize - floatAvailable;
            var entryUse = new Entry(floatUsed);


            var usedPercent = ((floatSize / floatUsed));

            entryUse.Color = SKColors.DarkOrange;
            if (usedPercent > 90)
                entryUse.Color = SKColors.DarkRed;
            if (usedPercent < 60)
                entryUse.Color = SKColors.DarkGreen;

            Entries = new Entry[] { entrySize, entryUse };
            RadialGaugeChart = new RadialGaugeChart() { Entries = Entries };
        }

    }
}