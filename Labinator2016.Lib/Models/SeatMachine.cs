//-----------------------------------------------------------------------
// <copyright file="SeatMachine.cs" company="Interactive Intelligence">
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
    /// Database Model for the SeatMachine table
    /// </summary>
    public class SeatMachine
    {
        /// <summary>
        /// Gets or sets the seat machine identifier.
        /// </summary>
        /// <value>
        /// The seat machine identifier.
        /// </value>
        public int SeatMachineId { get; set; }

        /// <summary>
        /// Gets or sets the course machine identifier.
        /// </summary>
        /// <value>
        /// The course machine identifier.
        /// </value>
        public int CourseMachineId { get; set; }

        /// <summary>
        /// Gets or sets the seat identifier.
        /// </summary>
        /// <value>
        /// The seat identifier.
        /// </value>
        public int SeatId { get; set; }

        /// <summary>
        /// Gets or sets the vm identifier.
        /// </summary>
        /// <value>
        /// The vm identifier.
        /// </value>
        public string VMId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>
        /// The session identifier.
        /// </value>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the seat.
        /// </summary>
        /// <value>
        /// The seat.
        /// </value>
        public virtual Seat seat { get; set; }

        /// <summary>
        /// Gets or sets the course machine.
        /// </summary>
        /// <value>
        /// The course machine.
        /// </value>
        public virtual CourseMachine courseMachine { get; set; }
    }
}
