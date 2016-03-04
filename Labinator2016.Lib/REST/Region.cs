using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labinator2016.Lib.REST
{
    public static class Region
    {
        private static List<string> regionList = new List<string>() {  "US-West",
                                                                    "US-Central",
                                                                    "US-East",
                                                                    "EMEA",
                                                                    "APAC",
                                                                    "AUS-Sydney",
                                                                    "CAN-Toronto"
                                                                  };

        public static List<string> regions
        {
            get
            {
                return regionList;
            }
        }
    }
}