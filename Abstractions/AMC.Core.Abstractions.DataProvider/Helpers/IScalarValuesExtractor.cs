using System.Collections.Generic;
using System.Data;

namespace AMC.Core.Abstractions.DataProvider.Helpers
{
    public interface IScalarValuesExtractor
    {
        #region Int

        int Int(string query, params IDbDataParameter[] parameters);

        object Int(string query, CommandType type = CommandType.Text, params IDbDataParameter[] parameters);

        object Int(string query, CommandType type = CommandType.Text, IEnumerable<IDbDataParameter> parameters = null);

        object Int(QueryBuilder.IQueryBuilder Builder, CommandType type = CommandType.Text);

        #endregion

        #region Long

        long Long(string query, params IDbDataParameter[] parameters);

        long Long(string query, CommandType type = CommandType.Text, params IDbDataParameter[] parameters);

        long Long(string query, CommandType type = CommandType.Text, IEnumerable<IDbDataParameter> parameters = null);

        long Long(QueryBuilder.IQueryBuilder Builder, CommandType type = CommandType.Text);

        #endregion
    }
}
