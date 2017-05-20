using System.Text.RegularExpressions;
using Doods.LibSsh.Interfaces;

namespace Doods.LibSsh.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  vcgencmd measure_temp
    ///  temp=44.4'C
    /// </example>
    public class CpuTempQuery : GenericQuery<double>
    {

        private static Regex CPU_PATTERN = new Regex(@"[0-9.]{4,}");
        private static string CPUTEMP_CMD = " measure_temp";

        private string _vcgencmdPath;

        public CpuTempQuery(IClientSsh client, string vcgencmdPath) : base(client)
        {
            _vcgencmdPath = vcgencmdPath;
            CmdString = _vcgencmdPath + CPUTEMP_CMD;
        }

        protected override double PaseResult(string result)
        {
           return parseTemperature(result);
        }

        private double parseTemperature(string output)
        {

            Match match = CPU_PATTERN.Match(output);
            if (match.Success)
            {

                double temperature = double.Parse(match.Value);
                return temperature;
            }
            else
            {
                //LOGGER.error("Could not parse cpu temperature.");
                //LOGGER.error("Output of 'vcgencmd measure_temp': \n{}", output);
                return 0D;
            }
        }
    }
}
