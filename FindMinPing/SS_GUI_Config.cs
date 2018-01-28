using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMinPing
{
    class SS_GUI_Config
    {
        public string remarks { get; set; }
        public string id => System.Guid.NewGuid().ToString();
        public string server { get; set; }
        public string server_port { get; set; }
        public int server_udp_port => 0;
        public string password { get; set; }
        public string method { get; set; }
        public string protocol => "origin";
        public string protocolparam => "";
        public string obfs => "plain";
        public string obfsparam => "";
        public string remarks_base64 { get; set; }
        public string group => "";
        public bool enable => true;
        public bool udp_over_tcp => false;
    }
}
