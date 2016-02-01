//-----------------------------------------------------------------------
// <copyright file="EventLog.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Lib.Utilities
{
    using System.Diagnostics;
    using Labinator2016.Lib.Headers;

    /// <summary>
    /// Allows the logging of messages to the Windows Event Log.
    /// </summary>
    public static class EventLog
    {
        /// <summary>
        /// The event log handle
        /// </summary>
        private static System.Diagnostics.EventLog eventLog = null;

        /// <summary>
        /// Sets up the event log handle.
        /// </summary>
        public static void SetupEventLog()
        {
            eventLog = new System.Diagnostics.EventLog();
            eventLog.Source = "Skytapinator_Service";
            eventLog.Log = "Skytapinator";
        }

        /// <summary>
        /// Logs an event to the Windows Application log..
        /// </summary>
        /// <param name="message">The text of the log message.</param>
        /// <param name="type">Event type (Info/Warning/Error)</param>
        /// <param name="id">The numeric identifier from the EventIds enumeration.</param>
        public static void LogE(string message, EventLogEntryType type, EventIds id)
        {
            if (eventLog == null)
            {
                SetupEventLog();
            }

            eventLog.WriteEntry(message, type, (int)id);
        }
    }
}
