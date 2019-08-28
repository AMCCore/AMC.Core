using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMC.Core.Abstractions.Logger
{
    public interface ILogEntry
    {
        LoggingEventType Severity { get; }

        string Message { get; }

        Exception Exception { get; }
    }

    internal struct LogEntry : ILogEntry
    {
        public LoggingEventType Severity { get; }
        public string Message { get; }
        public Exception Exception { get; }

        public LogEntry(LoggingEventType severity, string message, Exception exception = null)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

            Severity = severity;
            Message = message;
            Exception = exception;
        }
    }
}
