//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// Version: 1.1 - Password Management fields added.
/// </summary>
namespace Labinator2016.Lib.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Database model for the User table
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the User identifier.
        /// </summary>
        /// <value>
        /// The User identifier.
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
        /// Gets or sets a value indicating whether this User is an instructor.
        /// </summary>
        /// <value>
        /// <c>true</c> if this User is an instructor; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Instructor")]
        public bool IsInstructor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this User is an administrator.
        /// </summary>
        /// <value>
        /// <c>true</c> if this User is an administrator; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Administrator")]
        public bool IsAdministrator { get; set; }

        /// <summary>
        /// Gets or sets the API key, used by Instructors and Administrators to connect to SkyTap.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        [Display(Name = "SkyTap API Key")]
        public string STAPIKey { get; set; }

        /// <summary>
        /// Gets or sets the existing password. This is used when changing the password. Not a database field.
        /// </summary>
        /// <value>
        /// The existing password.
        /// </value>
        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Existing Password")]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the first entry of the new password. This is used when setting or changing the password. Not a database field.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword1 { get; set; }

        /// <summary>
        /// Gets or sets the verification entry of the password. This is used when setting or changing the password. Not a database field.
        /// </summary>
        /// <value>
        /// The verified password entry.
        /// </value>
        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string NewPassword2 { get; set; }
    }
}
