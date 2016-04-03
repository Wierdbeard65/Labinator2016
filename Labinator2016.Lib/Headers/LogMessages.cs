//-----------------------------------------------------------------------
// <copyright file="LogMessages.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Lib.Headers
{
    /// <summary>
    /// The different categories of messages that can be written to the log.
    /// </summary>
    public enum LogMessages
    {
        /// <summary>
        /// An entity was created.
        /// </summary>
        create,

        /// <summary>
        /// An entity was deleted.
        /// </summary>
        delete,

        /// <summary>
        /// An entity was updated.
        /// </summary>
        update,

        /// <summary>
        /// Someone logged on.
        /// </summary>
        logon,

        /// <summary>
        /// Someone Logged off.
        /// </summary>
        logout,

        /// <summary>
        /// There was an incorrect logon attempt.
        /// </summary>
        incorrectlogon,

        /// <summary>
        /// An environment was suspended.
        /// </summary>
        suspended,

        /// <summary>
        /// An environment was started.
        /// </summary>
        started,

        /// <summary>
        /// A User connected to an environment.
        /// </summary>
        connected,

        /// <summary>
        /// A User joined an existing environment connection.
        /// </summary>
        joined,

        /// <summary>
        /// An Error Occurred.
        /// </summary>
        Error,

        /// <summary>
        /// The service started.
        /// </summary>
        start,

        /// <summary>
        /// The service stopped.
        /// </summary>
        stop,

        /// <summary>
        /// Commencing a scan of environments to start or stop
        /// </summary>
        startstopscancommenced,

        /// <summary>
        /// The scan of environments to start or stop is complete
        /// </summary>
        startstopscancomplete,

        /// <summary>
        /// Commencing a scan of environments to start or stop
        /// </summary>
        cleanupscancommenced,

        /// <summary>
        /// The scan of environments to start or stop is complete
        /// </summary>
        cleanupscancomplete
    }
}
