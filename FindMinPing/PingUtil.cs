using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace FindMinPing
{
    class PingUtil
    {
        private static bool Ping(string address, out string time)
        {
            time = "Fail";
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(address);
            if (pingReply.Status == IPStatus.Success)
            {
                time = pingReply.RoundtripTime.ToString();
                return true;
            }
            if(pingReply.Status == IPStatus.TimedOut)
            {
                time = "TimeOut";
                return false;
            }
            return false;
        }

        public static IList<string> Ping(string address)
        {
            IList<string> ret = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                Ping(address, out var tmp);
                ret.Add(tmp);
            }
            return ret;
        }
    }
}
