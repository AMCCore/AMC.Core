using System;
using System.Collections.Generic;
using System.Data;

using AMC.Core.Abstractions.DataProvider.Helpers;

namespace AMC.Core.Abstractions.DataProvider
{
    public interface IDataStorage : IDisposable
    {
        IDataHelper Helper { get; }

        event EventHandler<EventArgs.ErrorEventArgs> Error;

        IScalarValuesExtractor Scalar { get; }

        #region ExecuteQuery

        object ExecuteQuery(string query, params IDbDataParameter[] parameters);

        object ExecuteQuery(string query, CommandType type = CommandType.Text, params IDbDataParameter[] parameters);

        object ExecuteQuery(string query, CommandType type = CommandType.Text, IEnumerable<IDbDataParameter> parameters = null);

        object ExecuteQuery(QueryBuilder.IQueryBuilder Builder, CommandType type = CommandType.Text);

        #endregion

        #region ExecuteNonQuery

        int ExecuteNonQuery(string query, params IDbDataParameter[] parameters);

        int ExecuteNonQuery(string query, CommandType type = CommandType.Text, params IDbDataParameter[] parameters);

        int ExecuteNonQuery(string query, CommandType type = CommandType.Text, IEnumerable<IDbDataParameter> parameters = null);

        int ExecuteNonQuery(QueryBuilder.IQueryBuilder Builder, CommandType type = CommandType.Text);

        #endregion

        #region ExecuteProcedure

        object ExecuteProcedure(string procedureName, params IDbDataParameter[] parameters);

        object ExecuteProcedure(string procedureName, IEnumerable<IDbDataParameter> parameters);

        #endregion
    }
}
