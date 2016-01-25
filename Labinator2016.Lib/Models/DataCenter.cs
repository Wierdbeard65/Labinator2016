//-----------------------------------------------------------------------
// <copyright file="DataCenter.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Lib.Models
{
    using System;

    /// <summary>
    /// Database model for the DataCenter table
    /// </summary>
    public class DataCenter
    {
        /// <summary>
        /// Gets or sets the data center identifier.
        /// </summary>
        /// <value>
        /// The data center identifier.
        /// </value>
        public int DataCenterId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the that classes associated with this DataCenter typically run in..
        /// </summary>
        /// <value>
        /// The timezone.
        /// </value>
        public String Timezone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether DataCenter is Skytap-based of Hyper-V
        /// </summary>
        /// <value>
        ///   <c>true</c> if Hyper-V; <c>false</c> if SkyTap.
        /// </value>
        public Boolean Type { get; set; }

        /// <summary>
        /// Gets or sets the Gateway IP Address
        /// </summary>
        /// <value>
        /// The IP address of the Spark Gateway used for this Datacenter.
        /// </value>
        public string GateWayIP { get; set; }
    }
}
