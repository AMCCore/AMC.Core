using AMC.Core.Abstractions.Cache.Repository;
using AMC.Core.Abstractions.DataProvider;
using AMC.Core.Abstractions.Logger;
using AMC.Core.Abstractions.QuantumBasis;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.QuantumDataProvider
{
    public abstract class QuantumStorage : IDisposable
    {
        private readonly BaseDataStorage _storage;
        private readonly ICacheRepository _cacheRepository;
        private readonly ILogger _logger;

        public QuantumStorage(BaseDataStorage Storage, ICacheRepository CacheRepository, ILoggerFactory LoggerFactory)
        {
            _storage = Storage;
            _cacheRepository = CacheRepository;
            _logger = LoggerFactory.Create(typeof(QuantumStorage));
        }

        public virtual void Dispose()
        {
            _storage?.Dispose();
        }
    }

    public sealed class QuantumStorage<T> : QuantumStorage where T : BaseQuantum
    {
        public QuantumStorage(BaseDataStorage Storage, ICacheRepository CacheRepository, ILoggerFactory LoggerFactory) : base(Storage, CacheRepository, LoggerFactory)
        {

        }

    }
}
