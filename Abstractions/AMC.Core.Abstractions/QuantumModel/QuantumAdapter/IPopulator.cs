using AMC.Core.Abstractions.DataProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumModel.QuantumAdapter
{
    public interface IPopulator<T> where T : IQuant
    {
        void CreateOrUpdate(IDataStorage dataStorage, T entiity);

        T Load(IDataStorage dataStorage, long Id);

        void Delete(IDataStorage dataStorage, T entiity);

        T GetCacheble(long Id);
    }
}
