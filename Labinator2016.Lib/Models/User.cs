//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Lib.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Database model for the User table
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is an instructor.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is an instructor; otherwise, <c>false</c>.
        /// </value>
        public bool IsInstructor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is an administrator.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is an administrator; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdministrator { get; set; }
    }
}
