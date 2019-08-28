using System;
using System.Collections.Generic;
using System.Text;

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
}
