//-----------------------------------------------------------------------
// <copyright file="FormsAuthWrapper.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// </summary>
namespace Labinator2016.Lib.Utility
{
    using System.Web.Security;
    using Headers;

    /// <summary>
    /// Implementation of Authentication interface for live use.
    /// Author: Paul Simpson
    /// </summary>
    /// <seealso cref="Labinator2016.Controllers.IAuth" />
    public class FormsAuthWrapper : IAuth
    {
        /// <summary>
        /// Passes the authentication through to the standard authenticator
        /// </summary>
        /// <param name="userName">User to authenticate</param>
        /// <param name="remember">Flag as to whether to remember user between logins</param>
        public void DoAuth(string userName, bool remember)
        {
            FormsAuthentication.SetAuthCookie(userName, remember);
        }

        /// <summary>
        /// Passes the authentication through to the standard authenticator
        /// </summary>
        public void DoDeAuth()
        {
            FormsAuthentication.SignOut();
        }
    }
}