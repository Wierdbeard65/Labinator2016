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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
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
        /// Gets or sets the user sparking this update (if any).
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public string User { get; set; }

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

        [NotMapped]
        public string jsTime
        {
            get
            {
                return this.TimeStamp.ToShortDateString() + " " + this.TimeStamp.ToLongTimeString();
            }
        }

        [NotMapped]
        public string Msg
        {
            get
            {
                return this.Message.ToString();
            }
        }

        /// <summary>
        /// Writes a log message to the database.
        /// </summary>
        /// <param name="db">The database handle.</param>
        /// <param name="logEntry">The log entry to write.</param>
        public static void Write(ILabinatorDb db, HttpContextBase cx, Log logEntry)
        {
            if ((logEntry.User==null) ||(logEntry.User == string.Empty))
            {
                logEntry.User = cx.User.Identity.Name;
            }
            logEntry.TimeStamp = DateTime.Now;
            db.Add<Log>(logEntry);
            db.SaveChanges();
        }
    }
}