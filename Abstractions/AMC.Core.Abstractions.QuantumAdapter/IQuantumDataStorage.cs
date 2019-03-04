using AMC.Core.Abstractions.DataProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumAdapter
{
    public interface IQuantumDataStorage : IDataStorage
    {
        new IQuantumDataHelper Helper { get; }

        void QuantCreateOrUpdate<T>(T entiity) where T : Quantums.IQuant;

        T Load<T>(long Id) where T : Quantums.IQuant;

        void Delete<T>(T entiity) where T : Quantums.IQuant;
    }
}
