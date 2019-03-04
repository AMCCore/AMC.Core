using System;
using System.Collections.Generic;
using System.Data;

using AMC.Core.Abstractions.DataProvider;
using AMC.Core.Abstractions.DataProvider.EventArgs;
using AMC.Core.Abstractions.DataProvider.Helpers;
using AMC.Core.Abstractions.DataProvider.QueryBuilder;
using AMC.Core.Abstractions.DataProvider.Transactions;
using AMC.Core.Abstractions.Logger;
using AMC.Core.Abstractions.Logger.Extensions;

namespace AMC.Core.DataStorages.MSSQLDataProvider
{
    public class MSSQLDataStoage : ITransactDataStorage
    {
        protected struct SqlCommandProperties : IDisposable
        {
            public readonly IDbCommand Command;
            public readonly IDbConnection Connection;
            public bool CloseConnection;

            public SqlCommandProperties(IDbConnection conn, string query, CommandType type, bool closeConnection = true)
            {
                Connection = conn;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                Command = conn.CreateCommand();
                Command.CommandText = query;
                Command.CommandType = type;
                CloseConnection = closeConnection;
            }

            public System.Data.IDataParameterCollection Parameters
            {
                get
                {
                    return Command.Parameters;
                }
            }

            #region IDisposable Members

            public void Dispose()
            {
                Command.Dispose();
                if (CloseConnection)
                    Connection.Dispose();
            }

            #endregion
        }

        public MSSQLDataStoage(ILoggerFactory LoggerFactory) : this(new MSSQLDataHelper(LoggerFactory))
        {
        }

        protected MSSQLDataStoage(IDataHelper Helper)
        {
            this.Helper = Helper;
        }

        public IDataHelper Helper { get; }

        public IScalarValuesExtractor Scalar
        {
            get
            {
                if(_scalar == null)
                {
                    _scalar = new MSSQLScalatValueExtractor(this);
                }
                return _scalar;
            }
        }
        private IScalarValuesExtractor _scalar;

        public event EventHandler<ErrorEventArgs> Error;

        protected virtual void OnError(ErrorEventArgs args)
        {
            Helper.Logger?.Log(args.Exception);
            Error?.Invoke(this, args);
        }

        public IDbConnection Connection
        {
            get
            {
                if(_connection == null || _connection.State != ConnectionState.Open)
                {
                    _connection = Helper.OpenConnection();
                }
                return _connection;
            }
        }
        private IDbConnection _connection;

        public virtual void Dispose()
        {
            _connection?.Dispose();
        }

        private SqlCommandProperties PrepareCommand(string query, CommandType type, IEnumerable<IDbDataParameter> parameters)
        {
            var result = PrepareCommand(query, type);
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    result.Parameters.Add(p);
                }
            }
            return result;
        }

        protected virtual SqlCommandProperties PrepareCommand(string query, CommandType type)
        {
            return new SqlCommandProperties(Connection, query, type, true);
        }

        public int ExecuteNonQuery(string query, CommandType type = CommandType.Text, IEnumerable<IDbDataParameter> parameters = null)
        {
            using (SqlCommandProperties cp = PrepareCommand(query, type, parameters))
            {
                try
                {
                    int res = cp.Command.ExecuteNonQuery();
                    return res;
                }
                catch (Exception ex)
                {
                    ErrorEventArgs args = new ErrorEventArgs(ex);
                    OnError(args);
                    if (!args.SupressError) throw;

                    return default(int);
                }
            }
        }

        #region ExecuteNonQuery Ext

        public int ExecuteNonQuery(string query, params IDbDataParameter[] parameters)
        {
            return ExecuteNonQuery(query, CommandType.Text, (IEnumerable<IDbDataParameter>)parameters);
        }

        public int ExecuteNonQuery(string query, CommandType type = CommandType.Text, params IDbDataParameter[] parameters)
        {
            return ExecuteNonQuery(query, type, (IEnumerable<IDbDataParameter>)parameters);
        }

        public int ExecuteNonQuery(IQueryBuilder Builder, CommandType type = CommandType.Text)
        {
            return ExecuteNonQuery(Builder.SqlClause, type, Builder.Parameters);
        }

        #endregion

        public object ExecuteProcedure(string procedureName, IEnumerable<IDbDataParameter> parameters)
        {
            try
            {
                using (SqlCommandProperties cp = PrepareCommand(procedureName, CommandType.StoredProcedure, parameters))
                {
                    var res = Helper.CreateDataParameterWithValue(Helper.ReturnValueParameterName(), ParameterDirection.ReturnValue, null);
                    cp.Parameters.Add(res);
                    cp.Command.ExecuteScalar();
                    return res.Value;
                }
            }
            catch (Exception ex)
            {
                ErrorEventArgs args = new ErrorEventArgs(ex);
                OnError(args);
                if (!args.SupressError)
                    throw;

                return null;
            }
        }

        #region ExecuteProcedure Ext

        public object ExecuteProcedure(string procedureName, params IDbDataParameter[] parameters)
        {
            return ExecuteProcedure(procedureName, (IEnumerable<IDbDataParameter>)parameters);
        }

        #endregion

        public object ExecuteQuery(string query, CommandType type = CommandType.Text, IEnumerable<IDbDataParameter> parameters = null)
        {
            try
            {
                DataSet res = null;
                using (SqlCommandProperties cp = PrepareCommand(query, type, parameters))
                {
                    res = new DataSet();
                    IDataAdapter da = Helper.GetDefaultAdapter(cp.Command);
                    da.Fill(res);
                    if (da is IDisposable)
                    {
                        ((IDisposable)da).Dispose();
                    }
                    return res.Tables[0];
                }
            }
            catch (Exception ex)
            {
                ErrorEventArgs args = new ErrorEventArgs(ex);
                OnError(args);
                if (!args.SupressError)
                    throw ex;

                return null;
            }
        }

        #region ExecuteQuery Ext

        public object ExecuteQuery(string query, params IDbDataParameter[] parameters)
        {
            return ExecuteQuery(query, CommandType.Text, (IEnumerable<IDbDataParameter>)parameters);
        }

        public object ExecuteQuery(string query, CommandType type = CommandType.Text, params IDbDataParameter[] parameters)
        {
            return ExecuteQuery(query, CommandType.Text, (IEnumerable<IDbDataParameter>)parameters);
        }

        public object ExecuteQuery(IQueryBuilder Builder, CommandType type = CommandType.Text)
        {
            return ExecuteQuery(Builder.SqlClause, type, Builder.Parameters);
        }

        #endregion

        public IDataTransaction BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            return new MSSQLDataTransaction(Helper, level);
        }
    }
}
