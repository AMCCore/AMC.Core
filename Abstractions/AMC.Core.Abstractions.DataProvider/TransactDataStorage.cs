using System.Data;

namespace AMC.Core.Abstractions.DataProvider
{
    public abstract class TransactDataStorage : DataStorage
    {
        protected TransactDataStorage(DataHelper Helper) : base(Helper) { }

        public abstract DataTransaction BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted);
    }
}
