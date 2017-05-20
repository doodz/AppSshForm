using System;

namespace Doods.StdLibSsh.Enums
{
    public class Memory
    {

        public static Memory B => new Memory("Byte", "B", 1);
        public static Memory KB => new Memory("KiloByte", "KB", 1024);
        public static Memory MB => new Memory("MegaByte", "MB",1024 * 1024);
        public static Memory GB => new Memory("GigaByte", "GB", 1024 * 1024 * 1024);
        public static Memory TB => new Memory("TeraByte", "TB", 1099511627776);//1024 * 1024 * 1024 * 1024

        private string _longName;
        private string _shortName;
        private long _scale;


        public string LongName
        {
            get { return _longName; }
            set { _longName = value; }
        }

        public string ShortName
        {
            get { return _shortName; }
            set { _shortName = value; }
        }
        public long Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        private Memory(String name, String shortName, long scale)
        {
            _longName = name;
            _shortName = shortName;
            _scale = scale;
        }

    }
}
