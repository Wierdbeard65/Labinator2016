//-----------------------------------------------------------------------
// <copyright file="Publish_Sets.cs" company="Interactive Intelligence">
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
    /// Class that corresponds to a publish set in Sky Tap. Used to encapsulate the various REST parameters.
    /// </summary>
    public class Publish_Sets
    {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the set
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL for the set.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets the type of the set
        /// </summary>
        public string Publish_set_type { get; set; }

        /// <summary>
        /// Gets or sets a limit on the runtime
        /// </summary>
        public object Runtime_limit { get; set; }

        /// <summary>
        /// Gets or sets the remaining runtime
        /// </summary>
        public object Runtime_left_in_seconds { get; set; }

        /// <summary>
        /// Gets or sets the expiration date
        /// </summary>
        public object Expiration_date { get; set; }

        /// <summary>
        /// Gets or sets the time zone of the expiration date.
        /// </summary>
        public object Expiration_date_tz { get; set; }

        /// <summary>
        /// Gets or sets the start time of the set
        /// </summary>
        public object Start_time { get; set; }

        /// <summary>
        /// Gets or sets the end time of the set
        /// </summary>
        public object End_time { get; set; }

        /// <summary>
        /// Gets or sets the time zone for the set
        /// </summary>
        public object Time_zone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether multiple URLs are allowed
        /// </summary>
        public bool Multiple_URL { get; set; }

        /// <summary>
        /// Gets or sets a password object
        /// </summary>
        public object Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the smart client should be used.
        /// </summary>
        public bool Use_smart_client { get; set; }

        /// <summary>
        /// Gets or sets the list on VMs in this set.
        /// </summary>
        public Vm[] VMs { get; set; }
    }
}