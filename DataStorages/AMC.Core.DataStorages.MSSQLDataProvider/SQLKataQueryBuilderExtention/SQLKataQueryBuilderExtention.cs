using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using AMC.Core.Abstractions.DataProvider.QueryBuilder;
using SqlKata;
using SqlKata.Compilers;

namespace AMC.Core.DataStorages.MSSQLDataProvider.SQLKataQueryBuilderExtention
{
    public static class SQLKataQueryBuilderExtention
    {
        private struct SQLKataQueryBulder : IQueryBuilder
        {
            public string SqlClause { get; set; }

            public ICollection<IDbDataParameter> Parameters { get; set; }
        }

        public static IQueryBuilder GetQueryBuilder(this Query query)
        {
            var compiler = new SqlServerCompiler();
            SqlResult result = compiler.Compile(query);
            return new SQLKataQueryBulder() { SqlClause = result.Sql, Parameters = result.NamedBindings.AsParallel().Select(ss => (IDbDataParameter)new System.Data.SqlClient.SqlParameter(ss.Key, ss.Value)).ToList() };
        }


    }
}
