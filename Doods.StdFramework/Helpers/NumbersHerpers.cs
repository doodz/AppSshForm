using System.Globalization;
using System.Text;

namespace Doods.StdFramework.Helpers
{
    public static class NumbersHerpers
    {

        public static bool TryParseFloat(string input, out float myFloat)
        {
            myFloat = 0;
            if (string.IsNullOrWhiteSpace(input))
                return false;

            const string Numbers = "0123456789.";
            var numberBuilder = new StringBuilder();
            foreach (char c in input)
            {
                if (Numbers.IndexOf(c) > -1)
                    numberBuilder.Append(c);
            }
            return float.TryParse(numberBuilder.ToString(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out myFloat);
        }
    }
}
