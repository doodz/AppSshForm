﻿using System.Collections.Generic;

namespace Omv.Rpc.StdClient.Commands
{
    public class OmvCommand
    {

        public string Options { get; set; }
        public string ServiceName { get; set; }
        public string MethodName { get; set; }
        public IEnumerable<string> Params { get; set; }

    }
}
