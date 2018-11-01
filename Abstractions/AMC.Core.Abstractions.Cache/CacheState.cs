using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.Cache
{
    public enum CacheState : int
    {
        NoCache = 1,
        //Private = 2, //user-specific cache
        ShortTerm = 3,
        LongTerm = 4,
        Permanent = 5,

        Default = 0
    }
}
