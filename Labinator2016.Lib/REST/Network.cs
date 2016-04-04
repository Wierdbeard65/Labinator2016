//-----------------------------------------------------------------------
// <copyright file="Network.cs" company="Interactive Intelligence">
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
    /// Class that corresponds to a network in Sky Tap. Used to encapsulate the various REST parameters.
    /// </summary>
    public class Network
    {
        /// <summary>
        /// Gets or sets the Sky Tap identifier for this network.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the URL associated with this network
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets the name of the network
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the network
        /// </summary>
        public string Network_type { get; set; }

        /// <summary>
        /// Gets or sets the subnet mask
        /// </summary>
        public string Subnet { get; set; }

        /// <summary>
        /// Gets or sets the network number for the network
        /// </summary>
        public string Subnet_addr { get; set; }

        /// <summary>
        /// Gets or sets the size of the Subnet.
        /// </summary>
        public int Subnet_size { get; set; }

        /// <summary>
        /// Gets or sets the default gateway for the network.
        /// </summary>
        public string Gateway { get; set; }

        /// <summary>
        /// Gets or sets a primary DNS server for the network.
        /// </summary>
        public object Primary_nameserver { get; set; }

        /// <summary>
        /// Gets or sets a secondary DNS server for the network.
        /// </summary>
        public object Secondary_nameserver { get; set; }

        /// <summary>
        /// Gets or sets a string representing the region this network is part of.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets an array of VPNs this network is attached to.
        /// </summary>
        public object[] VPN_attachments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this network can participate in a tunnel.
        /// </summary>
        public bool Tunnelable { get; set; }

        /// <summary>
        /// Gets or sets an array of tunnels this network is connected to
        /// </summary>
        public Tunnel[] Tunnels { get; set; }

        /// <summary>
        /// Gets or sets the domain of the Network.
        /// </summary>
        public string Domain { get; set; }
    }
}