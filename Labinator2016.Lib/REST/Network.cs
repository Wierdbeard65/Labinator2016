using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labinator2016.Lib.REST
{
    public class Network
    {
        public string id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string network_type { get; set; }
        public string subnet { get; set; }
        public string subnet_addr { get; set; }
        public int subnet_size { get; set; }
        public string gateway { get; set; }
        public object primary_nameserver { get; set; }
        public object secondary_nameserver { get; set; }
        public string region { get; set; }
        public object[] vpn_attachments { get; set; }
        public bool tunnelable { get; set; }
        public Tunnel[] tunnels { get; set; }
        public string domain { get; set; }
    }
}