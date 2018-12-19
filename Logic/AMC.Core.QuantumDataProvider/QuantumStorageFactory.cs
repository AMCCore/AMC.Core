using AMC.Core.Abstractions.Cache.Repository;
using AMC.Core.Abstractions.DataProvider;
using AMC.Core.Abstractions.Logger;
using AMC.Core.Abstractions.QuantumBasis;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Logic.QuantumDataProvider
{
    public sealed class QuantumStorageFactory
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly ILoggerFactory _loggerFactory;

        public QuantumStorageFactory(ICacheRepository CacheRepository = null, ILoggerFactory LoggerFactory = null)
        {
            _cacheRepository = CacheRepository;
            _loggerFactory = LoggerFactory;
        }

        public QuantumStorage<T> GetQuantumStorage<T>(BaseDataStorage Storage, Abstractions.QuantumAdapter.IPopulator<T> Populator) where T : BaseQuantum
        {
            return new QuantumStorage<T>(Storage, Populator, _cacheRepository, _loggerFactory);
        }
    }
}
