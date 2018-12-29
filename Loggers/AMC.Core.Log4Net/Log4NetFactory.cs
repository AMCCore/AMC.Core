using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Log4Net
{
    public sealed class Log4NetFactory : Abstractions.Logger.ILoggerFactory
    {
        public Abstractions.Logger.ILogger Create(Type type)
        {
            return new Log4NetAdapter(type);
        }
    }
}
