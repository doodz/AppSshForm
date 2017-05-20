using Doods.LibSsh.Interfaces;

namespace Doods.LibSsh.Queries
{
    public class VoltsQuery : GenericQuery<double>
    {
        private string VOLTS_CMD = " measure_volts core";
        private string _vcgencmdPath;
        public VoltsQuery(IClientSsh client, string vcgencmdPath) : base(client)
        {

            _vcgencmdPath = vcgencmdPath;
            CmdString = _vcgencmdPath + VOLTS_CMD;
        }

        protected override double PaseResult(string result)
        {
            return FormatVolts(result);
        }
        private double FormatVolts(string output)
        {
            var splitted = output.Trim().Split('=');
            if (splitted.Length >= 2)
            {
                var voltsWithUnit = splitted[1];
                var volts = voltsWithUnit.Substring(0,
                    voltsWithUnit.Length - 1);
                return double.Parse(volts);
            }
            else
            {
                //LOGGER.error("Could not parse cpu voltage.");
                //LOGGER.error("Output of 'vcgencmd measure_volts core': \n{}",
                //    output);
                return 0D;
            }
        }
    }
}
