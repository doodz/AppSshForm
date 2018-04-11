using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Omv.Rpc.StdClient.Modules
{

    public abstract class BaseOmvParser
    {
        private string _filePath;
        internal BaseOmvParser(string filepath)
        {
            _filePath = filepath;
        }

        public void ParseFile()
        {



        }

        protected abstract void Parse(IEnumerable<string> contentFile);

    }
    /// <summary>
    /// /var/www/openmediavault/js/omv/module/admin/service/
    /// </summary>
    public class FormItemsParser : BaseOmvParser
    {
        public string RpcService { get; private set; }
        public string JsonForm { get; private set; }

        public FormItemsParser(string filepath) : base(filepath)
        {

        }
        protected override void Parse(IEnumerable<string> contentFile)
        {
            var getFormItems = false;
            var tab = 0;
            var obj = 0;

            var getFormItemsStr = string.Empty;
            foreach (var re in contentFile)
            {
                if (re.Trim().StartsWith("//") || re.Trim().StartsWith("/**") || re.Trim().StartsWith("*")) continue;

                if (re.Contains("rpcService"))
                    RpcService = re.Split(new[] { ':', ',' }, StringSplitOptions.RemoveEmptyEntries)[1];

                if (re.Contains("return"))
                {
                    getFormItems = true;
                    //getFormItemsStr += re;
                }
                if (getFormItems)
                {
                    tab += re.Count(s => s == '[');
                    tab -= re.Count(s => s == ']');
                    obj += re.Count(s => s == '{');
                    obj -= re.Count(s => s == '}');
                    getFormItemsStr += re.Trim();
                    if (tab == 0 && obj == 0)
                        getFormItems = false;

                }

            }
            var replacement = Regex.Replace(getFormItemsStr, @"\t|\n|\r|return|;", "");
            replacement = Regex.Replace(replacement, @"'\s*\+\s*'", "");
            replacement = Regex.Replace(replacement, @"""\s*\+\s*""", "");
            replacement = Regex.Replace(replacement, @"(new.*\({(.*)}\),)", "{$2},");

            replacement = replacement.Replace("_(\"", "\"");
            replacement = replacement.Replace("\")", "\"");
            replacement = replacement.Replace("_('", "\"");
            replacement = replacement.Replace("')", "\"");
            JsonForm = replacement;
        }
    }
}
