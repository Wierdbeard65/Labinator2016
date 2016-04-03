//-----------------------------------------------------------------------
// <copyright file="Seat.cs" company="Interactive Intelligence">
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
    /// Database Model for the Seat table
    /// </summary>
    public class Seat
    {
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
        /// Gets or sets the Identifier for the Configuration in Sky Tap that corresponds to this Seat.
        /// </summary>
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
        
        /// <summary>
        /// Gets or sets the User associated with this Seat.
        /// </summary>
        public virtual User User { get; set; }
        ////[NotMapped]
        ////public virtual Classroom Classroom { get; set; }
        ////[NotMapped]
        ////public string State { get; set; }

        /// <summary>
        /// Converts a Seat object into a Temporary Seat object
        /// </summary>
        /// <returns>A Temporary Seat Object</returns>
        public SeatTemp ToSeatTemp()
        {
            SeatTemp seatTemp = new SeatTemp();
            seatTemp.SeatId = this.SeatId;
            seatTemp.ClassroomId = this.ClassroomId;
            seatTemp.UserId = this.UserId;
            seatTemp.ConfigurationId = this.ConfigurationId;
            return seatTemp;
        }
    }
}