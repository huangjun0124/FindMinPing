using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class PingUtil
    {
        private static bool Ping(string address, out string time)
        {
            time = "Fail";
            Ping ping = new Ping();
            try
            {
                PingReply pingReply = ping.Send(address);
                if (pingReply.Status == IPStatus.Success)
                {
                    time = pingReply.RoundtripTime.ToString();
                    return true;
                }
                if (pingReply.Status == IPStatus.TimedOut)
                {
                    time = "TimeOut";
                    return false;
                }
            }
            catch (Exception e)
            {
            }
            
            return false;
        }

        public static IList<string> Ping(string address, int pingTime = 4)
        {
            IList<string> ret = new List<string>();
            for (int i = 0; i < pingTime; i++)
            {
                Ping(address, out var tmp);
                ret.Add(tmp);
            }
            return ret;
        }

        public static void AnalyzePingResult(IList<string> results, out int min, out int max, out int avg)
        {
            min = int.MaxValue;
            max = avg = 0;
            int avgCnt = 0;
            foreach (string result in results)
            {
                if (result != "Fail" && result != "TimeOut")
                {
                    var tmp = int.Parse(result);
                    avgCnt++;
                    avg += tmp;
                    if (tmp > max)
                    {
                        max = tmp;
                    }
                    if (tmp < min)
                    {
                        min = tmp;
                    }
                }
            }

            if (avgCnt > 0)
            {
                avg /= avgCnt;
            }
            else
            {
                avg = int.MaxValue;
            }
        }
    }
}
