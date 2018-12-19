using AMC.Core.Abstractions.Cache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AMC.Core.Abstractions.QuantumBasis
{
    public abstract class BaseQuantum : ICacheable
    {
        private class Zusul
        {
            private Dictionary<int, object> _dict = new Dictionary<int, object>();
            
            public object this[int index]
            {
                get
                {
                    return _dict[index];
                }
                set
                {
                    _dict[index] = value;
                }
            }
        }

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

        #region Events Ext

        public ulong? AuthorId
        {
            get
            {
                if (Events == null)
                    return null;

                return Events.OrderByDescending(ss => ss.EventDate).Select(ss => ss.UserId).First();
            }
        }

        public DateTime? Created
        {
            get
            {
                if (Events == null)
                    return null;

                return Events.OrderByDescending(ss => ss.EventDate).Select(ss => ss.EventDate).First();
            }
        }

        public ulong? UpdaterId
        {
            get
            {
                if (Events == null)
                    return null;

                return Events.OrderBy(ss => ss.EventDate).Select(ss => ss.UserId).First();
            }
        }

        public DateTime? Updated
        {
            get
            {
                if (Events == null)
                    return null;

                return Events.OrderBy(ss => ss.EventDate).Select(ss => ss.EventDate).First();
            }
        }

        #endregion

        #region ICacheable Members

        CacheState ICacheable.CacheState => GetCacheState();

        protected virtual CacheState GetCacheState()
        {
            return CacheState.LongTerm;
        }

        string ICacheable.CacheKey => GetCacheKey();

        protected virtual string GetCacheKey()
        {
            return Id.ToString();
        }

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
