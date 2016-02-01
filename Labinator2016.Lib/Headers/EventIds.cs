//-----------------------------------------------------------------------
// <copyright file="EventIds.cs" company="Interactive Intelligence">
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
    /// List of Event ID's used when writing to the Windows Event Lag.
    /// </summary>
    public enum EventIds
    {
        /// <summary>
        /// The starting
        /// </summary>
        Starting = 1000,

        /// <summary>
        /// The started
        /// </summary>
        Started,

        /// <summary>
        /// The stopping
        /// </summary>
        Stopping,

        /// <summary>
        /// The stopped
        /// </summary>
        Stopped,

        /// <summary>
        /// The scanning
        /// </summary>
        Scanning,

        /// <summary>
        /// The configuration create failure
        /// </summary>
        ConfigCreateFailure,

        /// <summary>
        /// The retrieve project failure
        /// </summary>
        RetrieveProjectFailure,

        /// <summary>
        /// The retrieve network identifier failure
        /// </summary>
        RetrieveNetworkIDFailure,

        /// <summary>
        /// The change project IP failure
        /// </summary>
        ChangeProjectIPFailure,

        /// <summary>
        /// The create tunnel failure
        /// </summary>
        CreateTunnelFailure,

        /// <summary>
        /// The scan started
        /// </summary>
        ScanStarted,

        /// <summary>
        /// The scan ended
        /// </summary>
        ScanEnded,

        /// <summary>
        /// The begin updating thumbnails
        /// </summary>
        BeginUpdatingThumbnails,

        /// <summary>
        /// The end updating thumbnails
        /// </summary>
        EndUpdatingThumbnails,

        /// <summary>
        /// The begin suspending finished
        /// </summary>
        BeginSuspendingFinished,

        /// <summary>
        /// The end suspending finished
        /// </summary>
        EndSuspendingFinished,

        /// <summary>
        /// The begin suspending future
        /// </summary>
        BeginSuspendingFuture,

        /// <summary>
        /// The end suspending future
        /// </summary>
        EndSuspendingFuture,

        /// <summary>
        /// The begin suspending out of hours
        /// </summary>
        BeginSuspendingOutOfHours,

        /// <summary>
        /// The end suspending out of hours
        /// </summary>
        EndSuspendingOutOfHours,

        /// <summary>
        /// The begin starting classroom
        /// </summary>
        BeginStartingClassroom,

        /// <summary>
        /// The end starting classroom
        /// </summary>
        EndStartingClassroom
    }
}
