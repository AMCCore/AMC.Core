using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider.QueryBuilder
{
    public interface IQueryBuilder
    {
        string SqlClause
        {
            get;
        }

        ICollection<System.Data.IDbDataParameter> Parameters
        {
            get;
        }
    }
}
