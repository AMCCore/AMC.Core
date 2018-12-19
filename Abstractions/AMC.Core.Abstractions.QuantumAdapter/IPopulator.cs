using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumAdapter
{
    public interface IPopulator<T> where T : QuantumBasis.BaseQuantum
    {
        DataProvider.QueryBuilder.IQueryBuilder Populate(T entiity);

        T Depopulate(DataProvider.QueryBuilder.IQueryBuilder entiity);
    }
}
