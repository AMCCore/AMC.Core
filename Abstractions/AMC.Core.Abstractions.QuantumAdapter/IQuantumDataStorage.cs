using AMC.Core.Abstractions.DataProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumAdapter
{
    public interface IQuantumDataStorage : IDataStorage
    {
        void QuantCreateOrUpdate<T>(T entiity) where T : IPopulatableQuantum<Quantums.IQuant>;

        T Load<T>(long Id) where T : IPopulatableQuantum<Quantums.IQuant>;

        void Delete<T>(T entiity) where T : IPopulatableQuantum<Quantums.IQuant>;
    }
}
