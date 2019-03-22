using AMC.Core.Abstractions.Cache;
using AMC.Core.Abstractions.QuantumModel;
using AMC.Core.Abstractions.QuantumModel.QuantumHistory;
using AMC.Core.Abstractions.QuantumModel.QuantumTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.BasicQuantums
{
    public abstract class AQuant : IQuant, IQuantumHistoryEventCollection
    {
        private const string _cacheKeyPattern = "Quant({0})";

        protected virtual string CacheKeyPattern()
        {
            return _cacheKeyPattern;
        }

        public ulong Id { get; protected set; }

        public uint TypeId { get; protected set; }

        public IQuantumType Type { get; }

        public IQuantumValueCollection Values { get; }

        public CacheState CacheState => CacheState.LongTerm;

        public string CacheKey => string.Format(CacheKeyPattern(), Id);

        public ICollection<IQuantumHistoryEvent> History => throw new NotImplementedException();

        public ulong CreatorId { get; }

        public Users.User Creator => throw new NotImplementedException();

        public DateTime DateCreation { get; }

        public ulong LastUpdaterId { get; }

        public Users.User LastUpdater => throw new NotImplementedException();

        public DateTime DateLastUpdate { get; }

        IQuant IQuantumHistoryEventCollection.Creator => Creator;

        IQuant IQuantumHistoryEventCollection.LastUpdater => LastUpdater;

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
