using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsErrorLogger
    {
        static string Source = "DVLD";
        public static void LogError(string message)
        {
            if(!EventLog.SourceExists(Source))
            {
                EventLog.CreateEventSource(Source, "Application");
            }

            EventLog.WriteEntry(Source, message, EventLogEntryType.Error);
        }
    }
}
