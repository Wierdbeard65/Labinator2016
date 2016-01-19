//-----------------------------------------------------------------------
// <copyright file="Log.cs" company="Interactive Intelligence">
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
    using Labinator2016.Lib.Headers;

    /// <summary>
    /// Database model for the Log table
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Gets or sets the log message identifier.
        /// </summary>
        /// <value>
        /// The log message identifier.
        /// </value>
        public int LogId { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the message detail.
        /// </summary>
        /// <value>
        /// The message detail.
        /// </value>
        public string Detail { get; set; }

        /// <summary>
        /// Gets or sets the message type
        /// </summary>
        /// <value>
        /// The message type
        /// </value>
        public LogMessages Message { get; set; }

        /// <summary>
        /// Writes a log message to the database.
        /// </summary>
        /// <param name="db">The database handle.</param>
        /// <param name="logEntry">The log entry to write.</param>
        public static void Write(ILabinatorDb db, Log logEntry)
        {
            logEntry.TimeStamp = DateTime.Now;
            db.Add<Log>(logEntry);
            db.SaveChanges();
        }
    }
}