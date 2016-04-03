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
        /// Gets or sets the Seat machine identifier.
        /// </summary>
        /// <value>
        /// The Seat machine identifier.
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
        /// Gets or sets the Seat identifier.
        /// </summary>
        /// <value>
        /// The Seat identifier.
        /// </value>
        public int SeatId { get; set; }

        /// <summary>
        /// Gets or sets the Virtual Machine identifier.
        /// </summary>
        /// <value>
        /// The Virtual Machine identifier.
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
        /// Gets or sets the Seat.
        /// </summary>
        /// <value>
        /// The Seat.
        /// </value>
        public virtual Seat Seat { get; set; }

        /// <summary>
        /// Gets or sets the course machine.
        /// </summary>
        /// <value>
        /// The course machine.
        /// </value>
        public virtual CourseMachine CourseMachine { get; set; }
    }
}
