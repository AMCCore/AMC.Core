using AMC.Core.Abstractions.Cache;
using AMC.Core.Abstractions.Cache.Repository;
using AMC.Core.Abstractions.DataProvider;
using AMC.Core.Abstractions.Logger;
using AMC.Core.Abstractions.QuantumBasis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AMC.Core.Logic.QuantumDataProvider
{
    public abstract class QuantumStorage : IDisposable
    {
        protected readonly BaseDataStorage _storage;
        protected readonly ICacheRepository _cacheRepository;
        protected readonly ILogger _logger;

        public QuantumStorage(BaseDataStorage Storage, ICacheRepository CacheRepository = null, ILoggerFactory LoggerFactory = null)
        {
            _storage = Storage;
            _cacheRepository = CacheRepository;
            _logger = LoggerFactory?.Create(typeof(QuantumStorage));
        }

        public virtual void Dispose()
        {
            _storage?.Dispose();
        }
    }

    public sealed class QuantumStorage<T> : QuantumStorage where T : BaseQuantum
    {
        public QuantumStorage(BaseDataStorage Storage, ICacheRepository CacheRepository = null, ILoggerFactory LoggerFactory = null) : base(Storage, CacheRepository, LoggerFactory)
        {
        }

        public void Save(T instance)
        {
            _cacheRepository?.Remove(instance);
            throw new NotImplementedException();
        }
            
        public void Delete(T instance)
        {
            _cacheRepository?.Remove(instance);
            throw new NotImplementedException();
        }

        public IEnumerable<T> Load(params ulong[] Id)
        {
            for(uint i = 0; i < Id.Length; i++ )
            {
                T _res = _cacheRepository?.Load(Activator.CreateInstance(typeof(T), new object[] { Id[i] }) as ICacheable) as T;
                if (_res == null)
                {
                    throw new NotImplementedException();
                    _cacheRepository?.Save(_res);
                }
                yield return _res;
            }
        }

        public T Load(ulong Id)
        {
            return Load(new ulong[1] { Id }).FirstOrDefault();
        }

        public IEnumerable<T> Load(Func<ulong[]> loader)
        {
            return Load(loader());
        }
    }
}
