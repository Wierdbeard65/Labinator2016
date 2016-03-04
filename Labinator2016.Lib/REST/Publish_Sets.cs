using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labinator2016.Lib.REST
{
    public class Publish_Sets
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string publish_set_type { get; set; }
        public object runtime_limit { get; set; }
        public object runtime_left_in_seconds { get; set; }
        public object expiration_date { get; set; }
        public object expiration_date_tz { get; set; }
        public object start_time { get; set; }
        public object end_time { get; set; }
        public object time_zone { get; set; }
        public bool multiple_url { get; set; }
        public object password { get; set; }
        public bool use_smart_client { get; set; }
        public Vm[] vms { get; set; }
    }
}