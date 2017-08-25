using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Enums;
using Doods.StdLibSsh.Interfaces;

namespace Doods.StdLibSsh.Queries.GroupedQueries
{
    public class SystemInfoQueries : MultiGenericQueries<SystemBean>
    {
        public SystemInfoQueries(IClientSsh client) : base(client)
        {
            Action = CreateSystemBean;
        }

        private SystemBean CreateSystemBean()
        {
            var bean = new SystemBean();

            bean.DistributionName = new DistributionNameQuery(GetClient()).Run();
            bean.OsMemory = new MemoryQuery(GetClient()).Run();
            bean.CpuSerial = new SerialNoQuery(GetClient()).Run();
            bean.AverageLoad = new LoadAverageQuery(GetClient(), LoadAveragePeriod.FifteenMinutes).Run();
            bean.Uptime = new UptimeQuery(GetClient()).Run();
            return bean;
        }
    }
}