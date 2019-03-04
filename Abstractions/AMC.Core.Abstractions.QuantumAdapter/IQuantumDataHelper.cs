using AMC.Core.Abstractions.Cache.Repository;
using AMC.Core.Abstractions.DataProvider.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumAdapter
{
    public interface IQuantumDataHelper : IDataHelper
    {
        IPopulatorRepository PopulatorRepository { get; }

        ICacheRepository CacheRepository { get; }
    }
}
