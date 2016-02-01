//-----------------------------------------------------------------------
// <copyright file="CourseMachine.cs" company="Interactive Intelligence">
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
    /// Database model for the CourseMachine table
    /// </summary>
    public class CourseMachine
    {
        /// <summary>
        /// Gets or sets the course machine identifier.
        /// </summary>
        /// <value>
        /// The course machine identifier.
        /// </value>
        public int CourseMachineId { get; set; }

        /// <summary>
        /// Gets or sets the virtual machine identifier.
        /// </summary>
        /// <value>
        /// The virtual machine identifier.
        /// </value>
        public string VMId { get; set; }

        /// <summary>
        /// Gets or sets the course identifier.
        /// </summary>
        /// <value>
        /// The course identifier.
        /// </value>
        public int CourseId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
    }
}
