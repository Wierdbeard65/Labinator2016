//-----------------------------------------------------------------------
// <copyright file="Interface.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Lib.REST
{
    /// <summary>
    /// Class that corresponds to a Network Interface in Sky Tap. Used to encapsulate the various REST parameters.
    /// </summary>
    public class Interface
    {
        /// <summary>
        /// Gets or sets the identifier for the interface in Sky Tap
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the IP address for the interface.
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Gets or sets the hostname corresponding to this interface
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets the interface MAC address
        /// </summary>
        public string MAC { get; set; }

        /// <summary>
        /// Gets or sets a value indicating a service count.
        /// </summary>
        public int Services_count { get; set; }

        /// <summary>
        /// Gets or sets an array of services.
        /// </summary>
        public object[] Services { get; set; }

        /// <summary>
        /// Gets or sets a count of the number of public IPs associated with this interface
        /// </summary>
        public int Public_ips_count { get; set; }

        /// <summary>
        /// Gets or sets an array of public IP addresses associated with this interface
        /// </summary>
        public object[] Public_ips { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the Virtual Machine this interface resides on.
        /// </summary>
        public string VM_id { get; set; }

        /// <summary>
        /// Gets or sets the name of the Virtual Machine this interface resides on.
        /// </summary>
        public string VM_name { get; set; }

        /// <summary>
        /// Gets or sets the status of the interface
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the network this interface is attached to.
        /// </summary>
        public string Network_id { get; set; }

        /// <summary>
        /// Gets or sets the name of the network this interface is attached to.
        /// </summary>
        public string Network_name { get; set; }

        /// <summary>
        /// Gets or sets the type of the network this interface is attached to.
        /// </summary>
        public string Network_type { get; set; }

        /// <summary>
        /// Gets or sets the subnet of the network this interface is attached to.
        /// </summary>
        public string Network_subnet { get; set; }

        /// <summary>
        /// Gets or sets the type of the interface card.
        /// </summary>
        public string Nic_type { get; set; }
    }
}