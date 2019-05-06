using AMC.Core.Abstractions.DataProvider.Helpers;
using AMC.Core.Abstractions.QuantumModel;
using AMC.Core.Abstractions.QuantumModel.QuantumAdapter;
using AMC.Core.DataStorages.MSSQLDataProvider;
using System;

namespace AMC.Core.DataStorages.MSSQLQuantumDataProvider
{
    public class MSSQLQuantumDataStoage : MSSQLDataStoage, IQuantumDataStorage
    {
        private const string _errortext = "Populator for class {0} not found";

        public MSSQLQuantumDataStoage(IQuantumDataHelper Helper) : base(Helper)
        {
            this.Helper = Helper;
        }

        public new IQuantumDataHelper Helper { get; }

        public void Delete<T>(T entiity) where T : IQuant
        {
            CheckPopulatorExists(typeof(T));
            var pop = Helper.PopulatorRepository[typeof(T)];

            pop.Delete(this, entiity);

            Helper.CacheRepository.Remove(entiity);
        }

        public T Load<T>(long Id) where T : IQuant
        {
            CheckPopulatorExists(typeof(T));
            var pop = Helper.PopulatorRepository[typeof(T)];

            var res = Helper.CacheRepository.Load(pop.GetCacheble(Id));
            if (res == null)
            {
                var _res = (T)pop.Load(this, Id);
                Helper.CacheRepository.Save(_res);
                return _res;
            }

            if (!(res is T))
                throw new ObjectNotFoundException(Id);

            return (T)res;
        }

        public void QuantCreateOrUpdate<T>(T entiity) where T : IQuant
        {
            CheckPopulatorExists(typeof(T));
            var pop = Helper.PopulatorRepository[typeof(T)];
            pop.CreateOrUpdate(this, entiity);

            Helper.CacheRepository.Remove(entiity);
        }

        private void CheckPopulatorExists(Type T)
        {
            if (!Helper.PopulatorRepository.ContainsKey(T))
                throw new Exception(string.Format(_errortext, T.Name));
        }

    }
}
