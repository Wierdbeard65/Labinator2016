using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labinator2016.Lib.Utilities
{
    public static class IPUtils
    {
        public static string NumericToStringIP(int ip)
        {
            string returnValue = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                returnValue = (ip & 0x000000ff).ToString("###") + "." + returnValue;
                ip = ip >> 8;
            }

            returnValue = returnValue.Substring(0, returnValue.Length - 1);
            return returnValue;
        }

    }
}
