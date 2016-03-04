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
        /// Gets or sets the Name.
        /// </summary>
        /// <value>
        /// The Name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the that classes associated with this DataCenter typically run in..
        /// </summary>
        /// <value>
        /// The time zone.
        /// </value>
        public string Timezone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether DataCenter is SkyTap based of Hyper-V
        /// </summary>
        /// <value>
        ///   <c>true</c> if Hyper-V; <c>false</c> if SkyTap.
        /// </value>
        public bool Type { get; set; }

        /// <summary>
        /// Gets or sets the Gateway IP Address
        /// </summary>
        /// <value>
        /// The IP address of the Spark Gateway used for this Datacenter.
        /// </value>
        public string GateWayIP { get; set; }

        /// <summary>
        /// Gets or sets the name of the Name of the Environment running the Spark Gateway
        /// </summary>
        public string GateWayName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the Environment running the Spark Gateway
        /// </summary>
        public string GateWayId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the Backbone Network on the GateWay Environment
        /// </summary>
        public string GateWayBackboneId { get; set; }
    }
}
