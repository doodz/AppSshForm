using System;
using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  cat /proc/uptime
    ///  1106378.63 4430593.31
    /// </example>
    public class UptimeQuery : GenericQuery<double>
    {
        private string UPTIME_CMD = "cat /proc/uptime";
        public UptimeQuery(IClientSsh client) : base(client)
        {
            CmdString = UPTIME_CMD;
        }

        protected override double PaseResult(string result)
        {
           return FormatUptime(result);
        }

        private double FormatUptime(string output)
        {
            var lines = output.Split('\n');
            foreach (var line in lines)
            {
                var split = line.Split(' ');
                if (split.Length == 2)
                {
                    try
                    {
                        return double.Parse(split[0]);
                    }
                    catch (FormatException e)
                    {
                        //LOGGER.debug("Skipping line: {}", line);
                    }
                }
                else
                {
                    //LOGGER.debug("Skipping line: {}", line);
                }
            }
            //LOGGER.error("Expected a different output of command: {}", UPTIME_CMD);
            //LOGGER.error("Actual output was: {}", output);
            return 0D;
        }
    }
}
