//-----------------------------------------------------------------------
// <copyright file="SeatTemp.cs" company="Interactive Intelligence">
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
    /// Database Model for the Temporary Seat table
    /// </summary>
    public class SeatTemp
    {
        /// <summary>
        /// Gets or sets the Seat temporary identifier.
        /// </summary>
        /// <value>
        /// The Seat temporary identifier.
        /// </value>
        public int SeatTempId { get; set; }

        /// <summary>
        /// Gets or sets the Seat identifier.
        /// </summary>
        /// <value>
        /// The Seat identifier.
        /// </value>
        public int SeatId { get; set; }

        /// <summary>
        /// Gets or sets the classroom identifier.
        /// </summary>
        /// <value>
        /// The classroom identifier.
        /// </value>
        public int ClassroomId { get; set; }

        /// <summary>
        /// Gets or sets the User identifier.
        /// </summary>
        /// <value>
        /// The User identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>
        /// The session identifier.
        /// </value>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp indicating when the record was last accessed. Used to clean up.
        /// </value>
        [Column(TypeName = "datetime2")]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the User.
        /// </summary>
        /// <value>
        /// The User.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or Sets the Configuration Identifier
        /// </summary>
        /// <value>
        /// The Configuration Identifier
        /// </value>
        public string ConfigurationId { get; set; }

        ////public DateTime? LastContact { get; set; }
        ////public string SessionId { get; set; }
        ////public int PercentageRunning { get; set; }
        ////public string IPAddress { get; set; }
        ////public string Thumbnail { get; set; }
        ////public bool Active { get; set; }
        ////public bool Allow247 { get; set; }
        ////public bool Running { get; set; }
        ////[NotMapped]
        ////public virtual User User { get; set; }
        ////[NotMapped]
        ////public virtual Classroom Classroom { get; set; }
        ////[NotMapped]
        ////public string State { get; set; }

        /// <summary>
        /// Converts a Temporary Seat into a Seat by copying the relevant information
        /// </summary>
        /// <returns>A Seat version of the Temporary Seat</returns>
        public Seat ToSeat()
        {
            Seat seat = new Seat();
            seat.SeatId = this.SeatId;
            seat.ClassroomId = this.ClassroomId;
            seat.UserId = this.UserId;
            seat.ConfigurationId = this.ConfigurationId;
            return seat;
        }
    }
}