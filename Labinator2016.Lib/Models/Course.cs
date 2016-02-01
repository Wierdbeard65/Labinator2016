//-----------------------------------------------------------------------
// <copyright file="Course.cs" company="Interactive Intelligence">
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
    /// Database model for the Courses table
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Gets or sets the course identifier.
        /// </summary>
        /// <value>
        /// The course identifier.
        /// </value>
        public int CourseId { get; set; }

        /// <summary>
        /// Gets or sets the name of the course.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of days the corse runs for.
        /// </summary>
        /// <value>
        /// The course length.
        /// </value>
        public int Days { get; set; }

        /// <summary>
        /// Gets or sets the number of hours the course runs for each day.
        /// </summary>
        /// <value>
        /// The day length
        /// </value>
        public int Hours { get; set; }

        /// <summary>
        /// Gets or sets the template identification for the template used by this course.
        /// </summary>
        /// <value>
        /// The template.
        /// </value>
        public string Template { get; set; }

        /// <summary>
        /// Gets or sets the start time for this course (local time).
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        [Column(TypeName = "datetime2")]
        public DateTime StartTime { get; set; }
        ////public virtual List<Classroom> Classrooms { get; set; }
        ////public virtual List<Machine> Machines { get; set; }
        ////public virtual List<Log> Logs { get; set; }        

        /// <summary>
        /// Gets or sets the name of the template used for this course. Not present in the database.
        /// </summary>
        /// <value>
        /// The name of the template.
        /// </value>
        [NotMapped]
        public string TemplateName { get; set; }
    }
}