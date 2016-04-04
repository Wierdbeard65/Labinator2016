//-----------------------------------------------------------------------
// <copyright file="Tunnel.cs" company="Interactive Intelligence">
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
    /// Class that corresponds to a network tunnel in Sky Tap. Used to encapsulate the various REST parameters.
    /// </summary>
    public class Tunnel
    {
        /// <summary>
        /// Gets or sets the tunnel identifier. 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the status of the tunnel
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets an object representing an error state.
        /// </summary>
        public object Error { get; set; }

        /// <summary>
        /// Gets or sets the network that is one end of the tunnel.
        /// </summary>
        public Network Source_network { get; set; }

        /// <summary>
        /// Gets or sets the network that is the other end of the tunnel.
        /// </summary>
        public Network Target_network { get; set; }
    }
}