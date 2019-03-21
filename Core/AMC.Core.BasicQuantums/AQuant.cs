using AMC.Core.Abstractions.Cache;
using AMC.Core.Abstractions.QuantumModel;
using AMC.Core.Abstractions.QuantumModel.QuantumTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.BasicQuantums
{
    public abstract class AQuant : IQuant
    {
        protected string CacheKeyPattern { get; }

        public ulong Id { get; protected set; }

        public uint TypeId { get; protected set; }

        public IQuantumType Type => throw new NotImplementedException();

        public IQuantumValueCollection Values => throw new NotImplementedException();

        public CacheState CacheState => CacheState.LongTerm;

        public string CacheKey => string.Format(CacheKeyPattern, Id);

        public virtual object LoadFromCache(object cachedData)
        {
            return cachedData;
        }

        public virtual object SaveToCache()
        {
            return this;
        }
    }
}
