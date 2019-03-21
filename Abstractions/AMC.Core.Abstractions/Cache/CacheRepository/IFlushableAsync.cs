using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMC.Core.Abstractions.Cache.Repository
{
    public interface IFlushableAsync : IFlushable
    {
        Task FlushCacheAsync();
    }
}
