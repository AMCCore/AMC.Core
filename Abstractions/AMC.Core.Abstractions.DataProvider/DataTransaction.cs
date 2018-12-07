using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider
{
    public abstract class DataTransaction : DataStorage
    {
        public SqlDataTransactionState State
        {
            get { return _state; }
        }

        protected IDbTransaction _tran;
        protected IDbConnection _connection;
        private SqlDataTransactionState _state;

        public EventHandler BeforeCommit;
        public EventHandler AfterCommit;
        public EventHandler BeforeRollback;
        public EventHandler AfterRollback;

        protected DataTransaction(DataHelper Helper) : this(IsolationLevel.ReadCommitted, Helper)
        {
        }

        protected DataTransaction(IsolationLevel level, DataHelper Helper) : base(Helper)
        {
            _connection = Helper.OpenConnection();
            _tran = _connection.BeginTransaction(level);
            _state = SqlDataTransactionState.Open;
        }

        public override void Dispose()
        {
            _tran.Dispose();
            _connection.Dispose();
            if (State != SqlDataTransactionState.Committed && State != SqlDataTransactionState.Error)
                _state = SqlDataTransactionState.RollBack;
        }

        public void Commit()
        {
            if (State != SqlDataTransactionState.Open)
                throw new Exception("Can not commit transaction in state other then 'open'");
            OnCommit();
        }

        public void RollBack()
        {
            if (State != SqlDataTransactionState.Open)
                throw new Exception("Can not rollback transaction in state other then 'open'");
            OnRollBack();
        }

        protected virtual void OnCommit()
        {
            BeforeCommit?.Invoke(this, System.EventArgs.Empty);
            _tran.Commit();
            _state = SqlDataTransactionState.Committed;
            AfterCommit?.Invoke(this, System.EventArgs.Empty);
        }

        protected virtual void OnRollBack()
        {
            BeforeRollback?.Invoke(this, System.EventArgs.Empty);
            _tran.Rollback();
            _state = SqlDataTransactionState.RollBack;
            AfterRollback?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
