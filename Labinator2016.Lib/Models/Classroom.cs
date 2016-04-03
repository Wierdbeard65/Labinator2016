//-----------------------------------------------------------------------
// <copyright file="Classroom.cs" company="Interactive Intelligence">
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
    /// The Classroom object represents a record in the Classroom table in the database.
    /// </summary>
    /// A Classroom is an instance of a specific <see cref="Course"/>, which has a series of <see cref="Seat"/> objectss (student environments)
    /// attached to it. It also has a <see cref="DataCenter"/> in which it is running and a Time and Date to start.
    public class Classroom
    {
        /// <summary>
        /// Gets or sets the Classroom identifier which uniquely identifies a specific Classroom
        /// </summary>
        public int ClassroomId { get; set; }

        /// <summary>
        /// Gets or sets the indentifier of the <see cref="Course"/> this Classroom is teaching
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Gets or sets the Identifier of the <see cref="DataCenter"/> this Classroom is housed in
        /// </summary>
        public int DataCenterId { get; set; }

        /// <summary>
        /// Gets or sets the indentifier of the <see cref="User"/> who is the primary Instructor for this Classroom
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the Identifier for the Sky Tap <see cref="Project"/> corresponding to the Classroom
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// Gets or sets the start date and time for the Class
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets a representation (not actually held in the Database record) of the <see cref="Course"/>
        /// </summary>
        public virtual Course Course { get; set; }

        /// <summary>
        /// Gets or sets a representation (not actually held in the Database record) of the <see cref="DataCenter"/>
        /// </summary>
        public virtual DataCenter DataCenter { get; set; }

        /// <summary>
        /// Gets a Javascript-readable string representation of the Start date for display.
        /// </summary>
        [NotMapped]
        public string JSDate
        {
            get
            {
                return this.Start.ToShortDateString();
            }
        }
    }
}