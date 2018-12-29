using System;
using System.Data;

using AMC.Core.Abstractions.DataProvider;
using AMC.Core.Abstractions.DataProvider.Helpers;
using AMC.Core.Abstractions.DataProvider.Transactions;


namespace AMC.Core.DataStorages.MSSQLDataProvider
{
    public sealed class MSSQLDataTransaction : MSSQLDataStoage, IDataTransaction
    {
        internal MSSQLDataTransaction(IDataHelper Helper, IsolationLevel Level = IsolationLevel.ReadCommitted) : base(Helper)
        {
            _tran = Connection.BeginTransaction(Level);
            State = SqlDataTransactionState.Open;
        }

        private IDbTransaction _tran;

        public SqlDataTransactionState State { get; private set; }

        public EventHandler BeforeCommit { get; set; }

        public EventHandler AfterCommit { get; set; }

        public EventHandler BeforeRollback { get; set; }

        public EventHandler AfterRollback { get; set; }

        public void Commit()
        {
            if (State != SqlDataTransactionState.Open)
                throw new TransactionStateException(nameof(Commit), SqlDataTransactionState.Open.ToString());
            OnCommit();
        }

        private void OnCommit()
        {
            BeforeCommit?.Invoke(this, EventArgs.Empty);
            _tran.Commit();
            State = SqlDataTransactionState.Committed;
            AfterCommit?.Invoke(this, EventArgs.Empty);
        }

        public void RollBack()
        {
            if (State != SqlDataTransactionState.Open)
                throw new TransactionStateException(nameof(RollBack), SqlDataTransactionState.Open.ToString());
            OnRollBack();
        }

        private void OnRollBack()
        {
            BeforeRollback?.Invoke(this, EventArgs.Empty);
            _tran.Rollback();
            State = SqlDataTransactionState.RollBack;
            AfterRollback?.Invoke(this, EventArgs.Empty);
        }

        protected override SqlCommandProperties PrepareCommand(string query, CommandType type)
        {
            var res = new SqlCommandProperties(Connection, query, type, false);
            res.Command.Transaction = _tran;
            return res;
        }

        public override void Dispose()
        {
            _tran.Dispose();
            base.Dispose();
            if (State != SqlDataTransactionState.Committed && State != SqlDataTransactionState.Error)
                State = SqlDataTransactionState.RollBack;
        }
    }
}
