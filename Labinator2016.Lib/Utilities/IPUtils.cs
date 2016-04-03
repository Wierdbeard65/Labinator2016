//-----------------------------------------------------------------------
// <copyright file="IPUtils.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Lib.Utilities
{
    /// <summary>
    /// Static class containing methods used to manipulate IP addresses
    /// </summary>
    public static class IPUtils
    {
        /// <summary>
        /// Used to convert a numeric representation of an IP address into a String version (sotted notation)
        /// </summary>
        /// <param name="ip">The integer version of the IP address</param>
        /// <returns>A string containing the dotted notation IP address</returns>
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
