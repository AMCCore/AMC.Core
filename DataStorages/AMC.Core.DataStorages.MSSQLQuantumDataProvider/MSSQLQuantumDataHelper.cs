using AMC.Core.Abstractions.Cache.Repository;
using AMC.Core.Abstractions.Logger;
using AMC.Core.Abstractions.QuantumModel.QuantumAdapter;
using AMC.Core.DataStorages.MSSQLDataProvider;

namespace AMC.Core.DataStorages.MSSQLQuantumDataProvider
{
    public class MSSQLQuantumDataHelper : MSSQLDataHelper, IQuantumDataHelper
    {
        public MSSQLQuantumDataHelper(ILoggerFactory LoggerFactory, IPopulatorRepository PopulatorRepository, ICacheRepository CacheRepository, bool SupressError = false) : base(LoggerFactory, SupressError)
        {
            this.PopulatorRepository = PopulatorRepository;
            this.CacheRepository = CacheRepository;
        }

        public IPopulatorRepository PopulatorRepository { get; }

        public ICacheRepository CacheRepository { get; }
    }
}
