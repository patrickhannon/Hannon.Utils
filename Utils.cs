using System.Diagnostics;
using System.Web.Security;
using System;
using System.Text;

namespace Hannon.Utils
{
    public static class Utils
    {
        public static void LogToEventLog(string source, string message, EventLogEntryType type)
        {
            using (EventLog eventLog = new EventLog(source))
            {
                eventLog.Source = source;
                eventLog.WriteEntry(message, type, 101, 1);
            }
        }

        private const string Purpose = "Authentication Token";

        public static string Protect(string unprotectedText)
        {
            var unprotectedBytes = Encoding.UTF8.GetBytes(unprotectedText);
            var protectedBytes = MachineKey.Protect(unprotectedBytes, Purpose);
            var protectedText = Convert.ToBase64String(protectedBytes);
            return protectedText;
        }

        public static string Unprotect(string protectedText)
        {
            var protectedBytes = Convert.FromBase64String(protectedText);
            var unprotectedBytes = MachineKey.Unprotect(protectedBytes, Purpose);
            var unprotectedText = Encoding.UTF8.GetString(unprotectedBytes);
            return unprotectedText;
        }

    }
}
