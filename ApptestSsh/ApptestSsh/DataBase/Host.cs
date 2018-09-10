using Doods.StdRepository.Base;

namespace ApptestSsh.Core.DataBase
{
    public class Host : TableBase
    {
        public int Port { get; set; }

        public string HostName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public bool IsOmvServer { get; set; }
        public bool IsRpi { get; set; }
        public bool IsSsh { get; set; }
    }
}

