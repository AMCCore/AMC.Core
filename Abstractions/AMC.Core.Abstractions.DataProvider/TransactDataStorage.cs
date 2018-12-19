using System.Data;

namespace AMC.Core.Abstractions.DataProvider
{
    public abstract class TransactDataStorage : DataStorages.MainDataStorage
    {
        protected TransactDataStorage(DataHelper Helper) : base(Helper) { }

        public abstract DataTransaction BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted);
    }
}
