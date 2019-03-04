using System;
using System.Collections.Generic;
using System.Data;

using AMC.Core.Abstractions.DataProvider;
using AMC.Core.Abstractions.DataProvider.EventArgs;
using AMC.Core.Abstractions.DataProvider.Helpers;
using AMC.Core.Abstractions.DataProvider.QueryBuilder;
using AMC.Core.Abstractions.DataProvider.Transactions;
using AMC.Core.Abstractions.QuantumAdapter;
using AMC.Core.Abstractions.Quantums;
using AMC.Core.DataStorages.MSSQLDataProvider;

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

            ExecuteNonQuery(pop.Delete(entiity));

            Helper.CacheRepository.Remove(entiity);
        }

        public T Load<T>(long Id) where T : IQuant
        {
            CheckPopulatorExists(typeof(T));
            var pop = Helper.PopulatorRepository[typeof(T)];

            var res = Helper.CacheRepository.Load(pop.GetCacheble(Id));
            if (res == null)
            {
                var _res = (T)pop.Populate(ExecuteQuery(pop.BaseLoad(Id)));
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
            ExecuteNonQuery(pop.CreateOrUpdate(entiity));

            Helper.CacheRepository.Remove(entiity);
        }

        private void CheckPopulatorExists(Type T)
        {
            if (!Helper.PopulatorRepository.ContainsKey(T))
                throw new Exception(string.Format(_errortext, T.Name));
        }

    }
}
