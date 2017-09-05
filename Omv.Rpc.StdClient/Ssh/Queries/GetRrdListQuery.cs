using Doods.StdLibSsh.Base.Queries;
using Doods.StdLibSsh.Beans;
using Doods.StdLibSsh.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Omv.Rpc.StdClient.Ssh.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  ls -l /var/lib/openmediavault/rrd --time-style=long-iso
    ///  total 780
    ///  -rw-r--r-- 1 root root 13859 2017-09-05 12:00 cpu-0-day.png
    ///  -rw-r--r-- 1 root root 15879 2017-09-05 12:00 cpu-0-hour.png
    ///  -rw-r--r-- 1 root root 14146 2017-09-05 12:00 cpu-0-month.png
    ///  -rw-r--r-- 1 root root 14318 2017-09-05 12:00 cpu-0-week.png
    ///  -rw-r--r-- 1 root root 14426 2017-09-05 12:00 cpu-0-year.png
    ///  -rw-r--r-- 1 root root 11664 2017-09-05 12:00 df-media-05cc7fcc-2b6f-4afb-90f9-c42d7cccfb6e-day.png
    ///  -rw-r--r-- 1 root root 11293 2017-09-05 12:00 df-media-05cc7fcc-2b6f-4afb-90f9-c42d7cccfb6e-hour.png
    ///  -rw-r--r-- 1 root root 12786 2017-09-05 12:00 df-media-05cc7fcc-2b6f-4afb-90f9-c42d7cccfb6e-month.png
    ///  -rw-r--r-- 1 root root 12734 2017-09-05 12:00 df-media-05cc7fcc-2b6f-4afb-90f9-c42d7cccfb6e-week.png
    ///  -rw-r--r-- 1 root root 14356 2017-09-05 12:00 df-media-05cc7fcc-2b6f-4afb-90f9-c42d7cccfb6e-year.png
    ///  -rw-r--r-- 1 root root 12314 2017-09-05 12:00 df-media-91029316-1927-484a-8ab7-816042cac08b-day.png
    ///  -rw-r--r-- 1 root root 12660 2017-09-05 12:00 df-media-91029316-1927-484a-8ab7-816042cac08b-hour.png
    ///  -rw-r--r-- 1 root root 12999 2017-09-05 12:00 df-media-91029316-1927-484a-8ab7-816042cac08b-month.png
    ///  -rw-r--r-- 1 root root 13095 2017-09-05 12:00 df-media-91029316-1927-484a-8ab7-816042cac08b-week.png
    ///  -rw-r--r-- 1 root root 14473 2017-09-05 12:00 df-media-91029316-1927-484a-8ab7-816042cac08b-year.png
    ///  -rw-r--r-- 1 root root 11970 2017-09-05 12:00 df-root-day.png
    ///  -rw-r--r-- 1 root root 11311 2017-09-05 12:00 df-root-hour.png
    ///  -rw-r--r-- 1 root root 12560 2017-09-05 12:00 df-root-month.png
    ///  -rw-r--r-- 1 root root 12499 2017-09-05 12:00 df-root-week.png
    ///  -rw-r--r-- 1 root root 13778 2017-09-05 12:00 df-root-year.png
    ///  -rw-r--r-- 1 root root  9527 2017-09-05 12:00 df-srv-dev-disk-by-label-SAMSUNG-day.png
    ///  -rw-r--r-- 1 root root  9830 2017-09-05 12:00 df-srv-dev-disk-by-label-SAMSUNG-hour.png
    ///  -rw-r--r-- 1 root root 10006 2017-09-05 12:00 df-srv-dev-disk-by-label-SAMSUNG-month.png
    ///  -rw-r--r-- 1 root root 10103 2017-09-05 12:00 df-srv-dev-disk-by-label-SAMSUNG-week.png
    ///  -rw-r--r-- 1 root root 12422 2017-09-05 12:00 df-srv-dev-disk-by-label-SAMSUNG-year.png
    ///  -rw-r--r-- 1 root root 17071 2017-09-05 12:00 interface-eth0-day.png
    ///  -rw-r--r-- 1 root root 22775 2017-09-05 12:00 interface-eth0-hour.png
    ///  -rw-r--r-- 1 root root 21205 2017-09-05 12:00 interface-eth0-month.png
    ///  -rw-r--r-- 1 root root 16423 2017-09-05 12:00 interface-eth0-week.png
    ///  -rw-r--r-- 1 root root 26100 2017-09-05 12:00 interface-eth0-year.png
    ///  -rw-r--r-- 1 root root 19072 2017-09-05 12:00 load-day.png
    ///  -rw-r--r-- 1 root root 14729 2017-09-05 12:00 load-hour.png
    ///  -rw-r--r-- 1 root root 42660 2017-09-05 12:00 load-month.png
    ///  -rw-r--r-- 1 root root 27436 2017-09-05 12:00 load-week.png
    ///  -rw-r--r-- 1 root root 22866 2017-09-05 12:00 load-year.png
    ///  -rw-r--r-- 1 root root 16863 2017-09-05 12:00 memory-day.png
    ///  -rw-r--r-- 1 root root 16015 2017-09-05 12:00 memory-hour.png
    ///  -rw-r--r-- 1 root root 19586 2017-09-05 12:00 memory-month.png
    ///  -rw-r--r-- 1 root root 18924 2017-09-05 12:00 memory-week.png
    ///  -rw-r--r-- 1 root root 20044 2017-09-05 12:00 memory-year.png
    ///  -rw-r--r-- 1 root root 11357 2017-09-05 12:00 sensors-day.png
    ///  -rw-r--r-- 1 root root 11694 2017-09-05 12:00 sensors-hour.png
    ///  -rw-r--r-- 1 root root 12006 2017-09-05 12:00 sensors-month.png
    ///  -rw-r--r-- 1 root root 12404 2017-09-05 12:00 sensors-week.png
    ///  -rw-r--r-- 1 root root 12578 2017-09-05 12:00 sensors-year.png
    /// </example>
    public class GetRrdListQuery : GenericQuery<List<FileInfoBean>>
    {
        public static readonly string Path = "/var/lib/openmediavault/rrd";
        private static readonly string Query = $"ls -l {Path} --time-style=long-iso";
        public GetRrdListQuery(IClientSsh client) : base(client)
        {
            CmdString = Query;
        }

        protected override List<FileInfoBean> PaseResult(string result)
        {
            var lst = new List<FileInfoBean>();


            var lines = result.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);


            foreach (var line in lines.Skip(1)) // remove "total xxxx"
            {
                var split = line.Trim().Split(' ');
                var fileInfo = new FileInfoBean();
                fileInfo.Path = GetRrdListQuery.Path;
                fileInfo.AccessRights = split[0];
                fileInfo.Id = int.Parse(split[1]);
                fileInfo.Owner = split[2];
                fileInfo.Group = split[3];
                fileInfo.Size = long.Parse(split[4]);
                fileInfo.Date = DateTime.Parse(split[5]);
                fileInfo.Hour = split[6];
                fileInfo.Name = split[7];

                lst.Add(fileInfo);
            }

            return lst;
        }
    }
}
