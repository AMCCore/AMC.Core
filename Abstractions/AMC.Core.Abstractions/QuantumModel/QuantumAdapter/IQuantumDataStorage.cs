using AMC.Core.Abstractions.DataProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumModel.QuantumAdapter
{
    public interface IQuantumDataStorage : IDataStorage
    {
        new IQuantumDataHelper Helper { get; }

        void QuantCreateOrUpdate<T>(T entiity) where T : IQuant;

        T Load<T>(long Id) where T : IQuant;

        void Delete<T>(T entiity) where T : IQuant;
    }
}
