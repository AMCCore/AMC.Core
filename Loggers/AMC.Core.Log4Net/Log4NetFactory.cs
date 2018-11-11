using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Log4Net
{
    public sealed class Log4NetFactory : AMC.Core.Abstractions.Logger.ILoggerFactory
    {
        public AMC.Core.Abstractions.Logger.ILogger Create(Type type)
        {
            return new Log4NetAdapter(type);
        }
    }
}
