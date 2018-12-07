using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider
{
    public abstract class DataStorage : IDisposable
    {
        protected readonly DataHelper Helper;

        public event EventHandler<EventArgs.ErrorEventArgs> Error;

        protected virtual void OnError(EventArgs.ErrorEventArgs args)
        {
            Error?.Invoke(this, args);
        }

        protected DataStorage(DataHelper Helper)
        {
            _scalar = new Lazy<ScalarValuesExtractor>(() => new ScalarValuesExtractor(this), true);
            this.Helper = Helper;
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
            return new SqlCommandProperties(this.Helper.OpenConnection(), query, type, true);
        }

        #region Scalar

        public struct ScalarValuesExtractor
        {
            private readonly DataStorage _owner;

            internal ScalarValuesExtractor(DataStorage owner)
            {
                _owner = owner;
            }

            #region Int

            public int Int(string query, CommandType type, IEnumerable<System.Data.Common.DbParameter> parameters)
            {
                object o = _owner.ExecuteScalar(query, type, parameters);
                if (o == null)
                    throw new ApplicationException("Scalar query did not return any values");
                else if (DBNull.Value == o)
                    throw new ApplicationException("Scalar query returned DBNull value, 'integer' expected");
                return Convert.ToInt32(o);
            }

            #endregion

            #region Long

            public long Long(string query, CommandType type, IEnumerable<System.Data.Common.DbParameter> parameters)
            {
                object o = _owner.ExecuteScalar(query, type, parameters);
                if (o == null)
                    throw new ApplicationException("Scalar query did not return any values");
                else if (DBNull.Value == o)
                    throw new ApplicationException("Scalar query returned DBNull value, 'long' expected");
                return Convert.ToInt64(o);
            }

            #endregion

        }

        public ScalarValuesExtractor Scalar
        {
            get
            {
                return this._scalar.Value;
            }
        }
        private readonly Lazy<ScalarValuesExtractor> _scalar;

        #endregion

        #region ExecuteQuery

        public virtual DataTable ExecuteQuery(string query, params IDbDataParameter[] parameters)
        {
            return ExecuteQuery(query, CommandType.Text, parameters);
        }

        public virtual DataTable ExecuteQuery(string query, CommandType type, params IDbDataParameter[] parameters)
        {
            return ExecuteQuery(query, CommandType.Text, (IEnumerable<IDbDataParameter>)parameters);
        }

        public virtual DataTable ExecuteQuery(string query, CommandType type, IEnumerable<IDbDataParameter> parameters)
        {
            try
            {
                DataSet res = null;
                using (SqlCommandProperties cp = PrepareCommand(query, type, parameters))
                {
                    res = new DataSet();
                    IDataAdapter da = Helper.CreateAdapter(cp.Command);
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
                EventArgs.ErrorEventArgs args = new EventArgs.ErrorEventArgs(ex);
                OnError(args);
                if (!args.SupressError)
                    throw;
                else return new DataTable();
            }
        }

        public virtual DataTable ExecuteQuery(QueryBuilder.IQueryBuilder Builder, CommandType type = CommandType.Text)
        {
            return ExecuteQuery(Builder.SqlClause, type, Builder.Parameters);
        }

        #endregion

        #region ExecuteNonQuery

        public virtual int ExecuteNonQuery(string query, params IDbDataParameter[] parameters)
        {
            return ExecuteNonQuery(query, CommandType.Text, parameters);
        }

        public virtual int ExecuteNonQuery(string query, CommandType type, IEnumerable<IDbDataParameter> parameters)
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
                    EventArgs.ErrorEventArgs args = new EventArgs.ErrorEventArgs(ex);
                    OnError(args);
                    if (!args.SupressError) throw;
                    else return 0;
                }
            }
        }

        #endregion

        #region ExecuteScalar

        public virtual object ExecuteScalar(string query, params IDbDataParameter[] parameters)
        {
            return ExecuteScalar(query, CommandType.Text, parameters);
        }

        public virtual object ExecuteScalar(string query, CommandType type, IEnumerable<IDbDataParameter> parameters)
        {
            try
            {
                using (SqlCommandProperties cp = PrepareCommand(query, type, parameters))
                {
                    if (type == CommandType.StoredProcedure)
                    {
                        var res = this.Helper.CreateDataParameterWithValue(this.Helper.ReturnValueParameterName(), ParameterDirection.ReturnValue, null);
                        cp.Parameters.Add(res);
                        cp.Command.ExecuteScalar();
                        return res.Value;
                    }
                    else
                    {
                        return cp.Command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                EventArgs.ErrorEventArgs args = new EventArgs.ErrorEventArgs(ex);
                OnError(args);
                if (!args.SupressError)
                    throw;
                else return null;
            }
        }

        #endregion

        #region ExecuteProcedure

        public virtual object ExecuteProcedure(string procedureName, params IDbDataParameter[] parameters)
        {
            return ExecuteProcedure(procedureName, parameters);
        }

        public virtual object ExecuteProcedure(string procedureName, IEnumerable<IDbDataParameter> parameters)
        {
            return ExecuteScalar(procedureName, CommandType.StoredProcedure, parameters);
        }

        #endregion

        public abstract void Dispose();
    }
}
