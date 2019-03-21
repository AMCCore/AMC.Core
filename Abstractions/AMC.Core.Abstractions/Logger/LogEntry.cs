using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMC.Core.Abstractions.Logger
{
    public enum LoggingEventType : int
    {
        Debug,
        Information,
        Warning,
        Error,
        Fatal
    };

    public class LogEntry
    {
        public readonly LoggingEventType Severity;
        public readonly string Message;
        public readonly Exception Exception;

        public LogEntry(LoggingEventType severity, string message, Exception exception = null)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

            this.Severity = severity;
            this.Message = message;
            this.Exception = exception;
        }
    }
}
