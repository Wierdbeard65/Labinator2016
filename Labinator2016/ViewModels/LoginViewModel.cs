//-----------------------------------------------------------------------
// <copyright file="LoginViewModel.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model used to return login information from login view
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the Name of the User Logging in
        /// </summary>
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the User's Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the URL of the page causing the authentication request (where to go after successful login)
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
