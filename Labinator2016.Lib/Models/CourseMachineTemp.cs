//-----------------------------------------------------------------------
// <copyright file="CourseMachineTemp.cs" company="Interactive Intelligence">
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
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Database model for the temporary Course Machine table
    /// </summary>
    public class CourseMachineTemp
    {
        /// <summary>
        /// Gets or sets the course machine temporary identifier.
        /// </summary>
        /// <value>
        /// The course machine temporary identifier.
        /// </value>
        public int CourseMachineTempId { get; set; }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>
        /// The session identifier.
        /// </value>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the virtual machine identifier.
        /// </summary>
        /// <value>
        /// The virtual machine identifier.
        /// </value>
        public string VMId { get; set; }

        /// <summary>
        /// Gets or sets the name of the virtual machine.
        /// </summary>
        /// <value>
        /// The name of the virtual machine.
        /// </value>
        public string VMName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp indicating when the record was last accessed. Used to clean up.
        /// </value>
        [Column(TypeName = "datetime2")]
        public DateTime TimeStamp { get; set; }
    }
}
