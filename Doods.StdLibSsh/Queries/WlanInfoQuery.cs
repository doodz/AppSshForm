﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Doods.LibSsh.Beans;
using Doods.LibSsh.Interfaces;

namespace Doods.LibSsh.Queries
{
    public class WlanInfoQuery : GenericQuery<string>
    {
        private List<NetworkInterfaceInformation> _wirelessInterfaces;
        public WlanInfoQuery(IClientSsh client, List<NetworkInterfaceInformation> wirelessInterfaces) : base(client)
        {
            CmdString = "cat /proc/net/wireless";
            _wirelessInterfaces = wirelessInterfaces;
        }

        protected override string PaseResult(string result)
        {
            //LOGGER.debug("Real output of /proc/net/wireless: \n{}",output);
            return ParseWlan(result);
        }

        private string ParseWlan(string output)
        {
            var lines = output.Split('\n');
            foreach (var line in lines)
            {
                if (line.StartsWith("Inter-") || line.StartsWith(" face"))
                {
                    //LOGGER.debug("Skipping header line: {}", line);
                    continue;
                }

                var res = Regex.Matches(line, "\\s+").Cast<Match>()
                    .ToArray(); ;

                if (res.Length >= 11)
                {
                    //LOGGER.debug("Parsing output line: {}", line);
                    // getting interface name
                    var name = res[1].Value.Replace(":", string.Empty);
                    //LOGGER.debug("Parsed interface name: {}", name);
                    var linkQuality = res[3].Value.Replace(".", string.Empty);
                    //LOGGER.debug("LINK QUALITY>>>{}<<<", linkQuality);
                    var linkLevel = res[4].Value.Replace(".", string.Empty);
                    //LOGGER.debug("LINK LEVEL>>>{}<<<", linkLevel);

                    int? linkQualityInt = null;
                    try
                    {
                        linkQualityInt = int.Parse(linkQuality);
                    }
                    catch (FormatException e)
                    {
                        //LOGGER.warn( "Could not parse link quality field for input: {}.", linkQuality);
                    }
                    int? signalLevelInt = null;
                    try
                    {
                        signalLevelInt = int.Parse(linkLevel);
                    }
                    catch (Exception e)
                    {
                        //LOGGER.warn( "Could not parse link level field for input: {}.",linkLevel);
                    }
                   // LOGGER.debug( "WLAN status of {}: link quality {}, signal level {}.",
                   //     new Object[] { name, linkQualityInt, signalLevelInt });

                    foreach (var iface in _wirelessInterfaces)
                    {
                        if (iface.Name.Equals(name))
                        {
                            var wlanInfo = new WlanBean();
                            wlanInfo.LinkQuality =linkQualityInt.GetValueOrDefault();
                            wlanInfo.SignalLevel = signalLevelInt.GetValueOrDefault();
                            //LOGGER.debug( "Adding wifi-status info to interface {}.", iface.getName());
                            iface.WlanInfo = wlanInfo;
                        }
                    }
                }
            }
            return null;
        }
    }
}
