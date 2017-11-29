using Newtonsoft.Json.Linq;
using Omv.Rpc.StdClient.Commands;

namespace Omv.Rpc.StdClient.Services
{

    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// 
    /// {
    /// "service": "FileSystemMgmt",
    /// "method": "getList",
    /// "params": {
    /// "start": 0,
    /// "limit": 25,
    /// "sortfield": "devicefile",
    /// "sortdir": "ASC"
    /// },
    /// "options": {
    /// "updatelastaccess": false
    /// }
    /// }
    /// 
    /// {
    ///   "response": {
    ///     "total": 3,
    ///     "data": [
    ///       {
    ///         "devicefile": "/dev/sda1",
    ///         "parentdevicefile": "/dev/sda",
    ///         "uuid": "a917bf86-41ac-4036-8935-a4c2e10cc065",
    ///         "label": "",
    ///         "type": "ext4",
    ///         "blocks": "60344404",
    ///         "mounted": true,
    ///         "mountpoint": "/",
    ///         "used": "3.25 GiB",
    ///         "available": "55132561408",
    ///         "size": "61792669696",
    ///         "percentage": 6,
    ///         "description": "/dev/sda1 (51.34 GiB available)",
    ///         "propposixacl": true,
    ///         "propquota": true,
    ///         "propresize": true,
    ///         "propfstab": true,
    ///         "propreadonly": false,
    ///         "propcompress": false,
    ///         "propautodefrag": false,
    ///         "hasmultipledevices": false,
    ///         "devicefiles": [
    ///           "/dev/sda1"
    ///         ],
    ///         "_readonly": true,
    ///         "_used": true,
    ///         "status": 1
    ///       },
    ///       {
    ///         "devicefile": "/dev/sda3",
    ///         "parentdevicefile": "/dev/sda",
    ///         "uuid": "91029316-1927-484a-8ab7-816042cac08b",
    ///         "label": "",
    ///         "type": "ext4",
    ///         "blocks": "315834788",
    ///         "mounted": true,
    ///         "mountpoint": "/media/91029316-1927-484a-8ab7-816042cac08b",
    ///         "used": "31.89 GiB",
    ///         "available": "272672202752",
    ///         "size": "323414822912",
    ///         "percentage": 12,
    ///         "description": "/dev/sda3 (253.94 GiB available)",
    ///         "propposixacl": true,
    ///         "propquota": true,
    ///         "propresize": true,
    ///         "propfstab": true,
    ///         "propreadonly": false,
    ///         "propcompress": false,
    ///         "propautodefrag": false,
    ///         "hasmultipledevices": false,
    ///         "devicefiles": [
    ///           "/dev/sda3"
    ///         ],
    ///         "_readonly": false,
    ///         "_used": true,
    ///         "status": 1
    ///       },
    ///       {
    ///         "devicefile": "/dev/sdb1",
    ///         "parentdevicefile": "/dev/sdb",
    ///         "uuid": "05cc7fcc-2b6f-4afb-90f9-c42d7cccfb6e",
    ///         "label": "wdRed",
    ///         "type": "ext4",
    ///         "blocks": "2884152988",
    ///         "mounted": true,
    ///         "mountpoint": "/media/05cc7fcc-2b6f-4afb-90f9-c42d7cccfb6e",
    ///         "used": "2.12 TiB",
    ///         "available": "470521860096",
    ///         "size": "2953372659712",
    ///         "percentage": 84,
    ///         "description": "wdRed (438.20 GiB available)",
    ///         "propposixacl": true,
    ///         "propquota": true,
    ///         "propresize": true,
    ///         "propfstab": true,
    ///         "propreadonly": false,
    ///         "propcompress": false,
    ///         "propautodefrag": false,
    ///         "hasmultipledevices": false,
    ///         "devicefiles": [
    ///           "/dev/sdb1"
    ///         ],
    ///         "_readonly": false,
    ///         "_used": true,
    ///         "status": 1
    ///       }
    ///     ]
    ///   },
    ///   "error": null
    /// }
    /// </example>
    public static class FileSystemService
    {
        private const string ServiceName = "FileSystemMgmt";

        public static OmvCommand CreateEnumerateFileSystemCommand()
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "getList"
            };


            var paramsObj = new JObject();
            paramsObj.Add(new JProperty("start", 0));
            paramsObj.Add(new JProperty("limit", 25));
            paramsObj.Add(new JProperty("sortfield", "devicefile"));
            paramsObj.Add(new JProperty("sortdir", "ASC"));
            //TODO : doods: a revoir, C’est fonctionnelle mais pas pratique . :/
            cmd.Params = new[]
            {
                "\""+paramsObj.ToString().Replace("\"","\\\"")+"\""
            };
            return cmd;
        }

        public static OmvCommand CreateMountCommand(string deviceFile)
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "mount"
            };

            var paramsObj = new JObject();
            paramsObj.Add(new JProperty("fstab", true));
            paramsObj.Add(new JProperty("id", deviceFile));
            //TODO : doods: a revoir, C’est fonctionnelle mais pas pratique . :/
            cmd.Params = new[]
            {
                "\""+paramsObj.ToString().Replace("\"","\\\"")+"\""
            };

            return cmd;
        }

        public static OmvCommand CreateUmountCommand(string deviceFile)
        {
            var cmd = new OmvCommand
            {
                ServiceName = ServiceName,
                MethodName = "umount"
            };

            var paramsObj = new JObject();
            paramsObj.Add(new JProperty("fstab", true));
            paramsObj.Add(new JProperty("id", deviceFile));
            //TODO : doods: a revoir, C’est fonctionnelle mais pas pratique . :/
            cmd.Params = new[]
            {
                "\""+paramsObj.ToString().Replace("\"","\\\"")+"\""
            };

            return cmd;
        }
    }
}