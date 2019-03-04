using AMC.Core.Abstractions.Cache.Repository;
using AMC.Core.Abstractions.DataProvider;
using AMC.Core.Abstractions.QuantumAdapter;
using AMC.Core.DataStorages.MSSQLDataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class RootStorage : MSSQLDataStoage, IQuantumDataStorage
    {
        protected RootStorage(IQuantumDataHelper Helper) : base(Helper)
        {
            this.Helper = Helper;
        }

        public new IQuantumDataHelper Helper { get; }
            
        public void QuantCreateOrUpdate<T>(T entiity) where T : AMC.Core.Abstractions.Quantums.IQuant
        {
            if (!Helper.PopulatorRepository.ContainsKey(typeof(T)))
                throw new IndexOutOfRangeException("Populator not found");
            var pop = Helper.PopulatorRepository[typeof(T)];

            ExecuteNonQuery(pop.CreateOrUpdate(entiity));

            Helper.CacheRepository.Remove(entiity);

        }

        public T Load<T>(long Id) where T : AMC.Core.Abstractions.Quantums.IQuant
        {
            if (!Helper.PopulatorRepository.ContainsKey(typeof(T)))
                throw new IndexOutOfRangeException("Populator not found");
            var pop = Helper.PopulatorRepository[typeof(T)];

            var res = Helper.CacheRepository.Load(pop.GetCacheble(Id));
            if (res == null)
            {
                var _res = (T)pop.Populate(ExecuteQuery(pop.BaseLoad(Id)));
                Helper.CacheRepository.Save(_res);
                return _res;
            }
            
            if (!(res is T))
                throw new NullReferenceException("Object not found");

            return (T)res;
        }

        public void Delete<T>(T entiity) where T : AMC.Core.Abstractions.Quantums.IQuant
        {
            if (!Helper.PopulatorRepository.ContainsKey(typeof(T)))
                throw new IndexOutOfRangeException("Populator not found");
            var pop = Helper.PopulatorRepository[typeof(T)];

            ExecuteNonQuery(pop.Delete(entiity));

            Helper.CacheRepository.Remove(entiity);
        }
    }
}
