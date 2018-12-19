using AMC.Core.Abstractions.Cache;
using AMC.Core.Abstractions.Cache.Repository;
using AMC.Core.Abstractions.DataProvider;
using AMC.Core.Abstractions.Logger;
using AMC.Core.Abstractions.QuantumBasis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AMC.Core.Abstractions.QuantumAdapter;
using AMC.Core.Abstractions.Logger.Extensions;

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
        private const string _debugBegin = "Quantum ({0}) {1} begin";
        private const string _debugEnd = "Quantum ({0}) {1} ended";
        private const string _save = "save";
        private const string _load = "load";
        private const string _delete = "delete";

        private readonly IPopulator<T> _populator;

        public QuantumStorage(BaseDataStorage Storage, IPopulator<T> Populator, ICacheRepository CacheRepository = null, ILoggerFactory LoggerFactory = null) : base(Storage, CacheRepository, LoggerFactory)
        {
            _populator = Populator;
        }

        public void Save(T instance)
        {
            _logger?.Debug(string.Format(_debugBegin, instance.Id, _save));
            _cacheRepository?.Remove(instance);
            _storage?.CreateOrUpdate(_populator.Populate(instance));
            _logger?.Debug(string.Format(_debugEnd, instance.Id, _save));
        }
            
        public void Delete(T instance)
        {
            _logger?.Debug(string.Format(_debugBegin, instance.Id, _delete));
            _cacheRepository?.Remove(instance);
            _storage?.Delete(_populator.Populate(instance));
            _logger?.Debug(string.Format(_debugEnd, instance.Id, _delete));
        }

        public IEnumerable<T> Load(params ulong[] Id)
        {
            for(uint i = 0; i < Id.Length; i++ )
            {
                _logger?.Debug(string.Format(_debugBegin, Id[i], _load));
                T _res = _cacheRepository?.Load(Activator.CreateInstance(typeof(T), new object[] { Id[i] }) as ICacheable) as T;
                if (_res == null)
                {
                    _res = (T)_storage?.Load(_populator.Populate(default(T)));
                    _cacheRepository?.Save(_res);
                }
                _logger?.Debug(string.Format(_debugEnd, Id[i], _load));
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
