//-----------------------------------------------------------------------
// <copyright file="Region.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Lib.REST
{
    using System.Collections.Generic;

    /// <summary>
    /// Static class that holds a list of valid Sky Tap Regions.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// String list of Regions
        /// </summary>
        private static List<string> regionList = new List<string>()
                                                                  {
                                                                    "US-West",
                                                                    "US-Central",
                                                                    "US-East",
                                                                    "EMEA",
                                                                    "APAC",
                                                                    "AUS-Sydney",
                                                                    "CAN-Toronto"
                                                                  };

        /// <summary>
        /// Gets the list of Regions.
        /// </summary>
        public static List<string> Regions
        {
            get
            {
                return regionList;
            }
        }
    }
}