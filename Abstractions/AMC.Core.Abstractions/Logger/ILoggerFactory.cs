using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMC.Core.Abstractions.Logger
{
    public interface ILoggerFactory
    {
        ILogger Create(Type type);
    }
}
