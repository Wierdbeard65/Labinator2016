using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labinator2016.Lib.REST
{
    public class Tunnel
    {
        public string id { get; set; }
        public string status { get; set; }
        public object error { get; set; }
        public Network source_network { get; set; }
        public Network target_network { get; set; }
    }
}