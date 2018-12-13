using AMC.Core.Abstractions.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumBasis
{
    public abstract class BaseQuantum : ICacheable
    {
        private BaseQuantum()
        {
        }

        public BaseQuantum(QuantumTypes.BaseQuantumType QuantumType) : this()
        {
            _quantumType = QuantumType;
        }

        protected BaseQuantum(ulong Id)
        {
            this.Id = Id;
        }

        public ulong Id { get; private set; }

        public QuantumTypes.BaseQuantumType QuantumType
        {
            get
            {
                return _quantumType;
            }
        }
        protected QuantumTypes.BaseQuantumType _quantumType;

        public QuantumValueCollection Valuse { get; private set; }

        public IReadOnlyCollection<QuantumHistory.QuantumHistoryEvent> Events { get; private set; }

        #region ICacheable Members

        CacheState ICacheable.CacheState => CacheState.LongTerm;

        string ICacheable.CacheKey => Id.ToString();

        object ICacheable.SaveToCache()
        {
            return this;
        }

        object ICacheable.LoadFromCache(object cachedData)
        {
            return cachedData;
        }

        #endregion
    }
}
