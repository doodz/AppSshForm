using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Omv.Rpc.StdClient.Modules
{
    /// <summary>
    /// /usr/share/openmediavault/engined/rpc/
    /// </summary>
    public class RpcServiceParser : BaseOmvParser
    {

        public readonly ICollection<string> MethodsRpc = new List<string>();
        public RpcServiceParser(string filepath) : base(filepath)
        {
        }

        protected override void Parse(IEnumerable<string> contentFile)
        {
            foreach (var methodLine in contentFile.Where(x => x.Contains("registerMethod")))
            {
                var res = Regex.Match(methodLine, @"registerMethod\(""(.*)""\)");
                if (res.Success)
                {
                    MethodsRpc.Add(res.Value);
                }
            }

        }
    }
}
