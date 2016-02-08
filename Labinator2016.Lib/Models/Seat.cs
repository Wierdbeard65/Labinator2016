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
        /// Gets or sets the seat identifier.
        /// </summary>
        /// <value>
        /// The seat identifier.
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
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
        ////public string ConfigurationId { get; set; }
        ////public DateTime? LastContact { get; set; }
        public string SessionId { get; set; }
        ////public int PercentageRunning { get; set; }
        ////public string IPAddress { get; set; }
        ////public string Thumbnail { get; set; }
        ////public bool Active { get; set; }
        ////public bool Allow247 { get; set; }
        ////public bool Running { get; set; }
        ////[NotMapped]
        public virtual User User { get; set; }
        ////[NotMapped]
        ////public virtual Classroom Classroom { get; set; }
        ////[NotMapped]
        ////public string State { get; set; }
        public SeatTemp toSeatTemp()
        {
            SeatTemp seatTemp = new SeatTemp();
            seatTemp.SeatId = this.SeatId;
            seatTemp.ClassroomId = this.ClassroomId;
            seatTemp.UserId = this.UserId;
            return seatTemp;
        }
    }
}