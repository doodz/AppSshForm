using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  cat /etc/*-release | grep PRETTY_NAME
    ///  PRETTY_NAME="Raspbian GNU/Linux 8 (jessie)"
    /// </example>
    public class DistributionNameQuery : GenericQuery<string>
    {
        private static string N_A = "n/a";
        private static string DISTRIBUTION_CMD = "cat /etc/*-release | grep PRETTY_NAME";
        public DistributionNameQuery(IClientSsh client) : base(client)
        {
            CmdString = DISTRIBUTION_CMD;
        }
       
        protected override string PaseResult(string result)
        {
            return ParseDistribution(result);
        }
        private string ParseDistribution(string output)
        {
            string[] split = output.Trim().Split('=');
            if (split.Length >= 2)
            {
                string distriWithApostroph = split[1];
                string distri = distriWithApostroph.Replace("\"", "");
                return distri;
            }
            else
            {
                //LOGGER.error("Could not parse distribution. Make sure 'cat /etc/*-release' works on your distribution.");
                //LOGGER.error("Output of {}: \n{}", DISTRIBUTION_CMD, output);
                return N_A;
            }
        }
    }
}
