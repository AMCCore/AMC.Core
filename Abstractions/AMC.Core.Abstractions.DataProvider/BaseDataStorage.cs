using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider
{
    public abstract class BaseDataStorage : IDisposable
    {
        protected readonly DataHelper Helper;

        public event EventHandler<EventArgs.ErrorEventArgs> Error;

        protected virtual void OnError(EventArgs.ErrorEventArgs args)
        {
            Error?.Invoke(this, args);
        }

        protected BaseDataStorage(DataHelper Helper)
        {
            this.Helper = Helper;
        }

        protected virtual SqlCommandProperties PrepareCommand(string query, CommandType type, IEnumerable<IDbDataParameter> parameters)
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

        protected virtual SqlCommandProperties PrepareCommand(QueryBuilder.IQueryBuilder Builder, CommandType type = CommandType.Text)
        {
            return PrepareCommand(Builder.SqlClause, type, Builder.Parameters);
        }

        public virtual void CreateOrUpdate(QueryBuilder.IQueryBuilder Builder, CommandType type = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public virtual object Load(QueryBuilder.IQueryBuilder Builder, CommandType type = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(QueryBuilder.IQueryBuilder Builder, CommandType type = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public abstract void Dispose();
    }
}
