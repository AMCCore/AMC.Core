using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace AMC.Core.Log4Net
{
    public class Log4NetAdapter : Abstractions.Logger.ILogger
    {
        private readonly log4net.ILog _adaptee;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        internal Log4NetAdapter(Type type)
        {
            var logRepository = log4net.LogManager.GetRepository(Assembly.GetEntryAssembly());
            var fi = new FileInfo("..\\..\\log4net.config");
            log4net.Config.XmlConfigurator.Configure(logRepository, fi);

            _adaptee = log4net.LogManager.GetLogger(type);
        }

        void Abstractions.Logger.ILogger.Log(Abstractions.Logger.ILogEntry entry)
        {
            switch (entry.Severity)
            {
                case Abstractions.Logger.LoggingEventType.Debug:
                    _adaptee.Debug(entry.Message, entry.Exception);
                    break;
                case Abstractions.Logger.LoggingEventType.Information:
                    _adaptee.Info(entry.Message, entry.Exception);
                    break;
                case Abstractions.Logger.LoggingEventType.Warning:
                    _adaptee.Warn(entry.Message, entry.Exception);
                    break;
                case Abstractions.Logger.LoggingEventType.Error:
                    _adaptee.Error(entry.Message, entry.Exception);
                    break;
                default:
                    _adaptee.Fatal(entry.Message, entry.Exception);
                    break;
            }

        }
    }
}
