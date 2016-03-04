using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labinator2016.Lib.REST
{
    public class Interface
    {
        public string id { get; set; }
        public string ip { get; set; }
        public string hostname { get; set; }
        public string mac { get; set; }
        public int services_count { get; set; }
        public object[] services { get; set; }
        public int public_ips_count { get; set; }
        public object[] public_ips { get; set; }
        public string vm_id { get; set; }
        public string vm_name { get; set; }
        public string status { get; set; }
        public string network_id { get; set; }
        public string network_name { get; set; }
        public string network_type { get; set; }
        public string network_subnet { get; set; }
        public string nic_type { get; set; }
    }
}