using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.Cache
{
    public interface ICacheable
    {
        CacheState CacheState { get; }

        string CacheKey { get; }

        object SaveToCache();

        object LoadFromCache(object cachedData);
    }
}
