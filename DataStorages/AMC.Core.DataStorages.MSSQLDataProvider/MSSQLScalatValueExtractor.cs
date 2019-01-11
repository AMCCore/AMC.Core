using AMC.Core.Abstractions.DataProvider;
using AMC.Core.Abstractions.DataProvider.Helpers;
using AMC.Core.Abstractions.DataProvider.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AMC.Core.DataStorages.MSSQLDataProvider
{
    public class MSSQLScalatValueExtractor : IScalarValuesExtractor
    {
        private readonly IDataStorage _owner;

        internal MSSQLScalatValueExtractor(IDataStorage owner)
        {
            _owner = owner;
        }


        public int Int(string query, CommandType type = CommandType.Text, IEnumerable<IDbDataParameter> parameters = null)
        {
            throw new NotImplementedException();
        }


        public int Int(string query, params IDbDataParameter[] parameters)
        {
            return Int(query, CommandType.Text, (IEnumerable<IDbDataParameter>)parameters);
        }

        public int Int(string query, CommandType type = CommandType.Text, params IDbDataParameter[] parameters)
        {
            return Int(query, type, (IEnumerable<IDbDataParameter>)parameters);
        }

        public int Int(IQueryBuilder Builder, CommandType type = CommandType.Text)
        {
            return Int(Builder.SqlClause, type, Builder.Parameters);
        }


        public long Long(string query, CommandType type = CommandType.Text, IEnumerable<IDbDataParameter> parameters = null)
        {
            throw new NotImplementedException();
        }

        public long Long(string query, params IDbDataParameter[] parameters)
        {
            return Long(query, CommandType.Text, (IEnumerable<IDbDataParameter>)parameters);
        }

        public long Long(string query, CommandType type = CommandType.Text, params IDbDataParameter[] parameters)
        {
            return Long(query, type, (IEnumerable<IDbDataParameter>)parameters);
        }

        public long Long(IQueryBuilder Builder, CommandType type = CommandType.Text)
        {
            return Long(Builder.SqlClause, type, Builder.Parameters);
        }
    }
}
