using System;
using System.Diagnostics;
using Labinator2016.Lib.Headers;

namespace Labinator2016.Lib.Utilities
{
    static class EventLog
    {
        private static System.Diagnostics.EventLog eventLog = null;
        public static void SetupEventLog()
        {
            eventLog = new System.Diagnostics.EventLog();
            eventLog.Source = "Skytapinator_Service";
            eventLog.Log = "Skytapinator";

        }
        public static void LogE(String message, EventLogEntryType type, EventIds id)
        {
            if (eventLog == null)
            {
                SetupEventLog();
            }
            eventLog.WriteEntry(message, type, (int)id);
        }
    }
}
