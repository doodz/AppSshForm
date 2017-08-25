using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;
using System;
using System.Globalization;

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
            var lines = output.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var split = line.Split(' ');
                if (split.Length == 2)
                {
                    try
                    {
                        return double.Parse(split[0], CultureInfo.InvariantCulture);
                    }
                    catch (FormatException e)
                    {
                        Logger.Instance.Debug($"Skipping line: {line}");
                    }
                }
                else
                {
                    Logger.Instance.Debug($"Skipping line: {line}");
                }
            }
            Logger.Instance.Error($"Expected a different output of command: {UPTIME_CMD}");
            Logger.Instance.Error($"Actual output was: {output}");
            return 0D;
        }
    }
}
