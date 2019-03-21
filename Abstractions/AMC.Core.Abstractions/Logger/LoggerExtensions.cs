using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMC.Core.Abstractions.Logger.Extensions
{
    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Information, message));
        }

        public static void Log(this ILogger logger, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Error, exception.Message, exception));
        }

        public static void Debug(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Debug, message));
        }

        public static void Warn(this ILogger logger, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Warning, exception.Message, exception));
        }

        public static void Warn(this ILogger logger, string Message, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Warning, Message, exception));
        }


        // More methods here.
    }
}
