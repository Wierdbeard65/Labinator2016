//-----------------------------------------------------------------------
// <copyright file="IAuth.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// </summary>
namespace Labinator2016.Lib.Headers
{
    /// <summary>
    /// Interface for Authentication.
    /// </summary>
    public interface IAuth
    {
        /// <summary>
        /// Does the authentication.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="remember">if set to <c>true</c> [remember].</param>
        void DoAuth(string userName, bool remember);

        /// <summary>
        /// Does the authentication.
        /// </summary>
        void DoDeAuth();
    }
}