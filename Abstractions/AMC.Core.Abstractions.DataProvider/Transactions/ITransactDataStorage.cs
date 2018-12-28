using System.Data;


namespace AMC.Core.Abstractions.DataProvider.Transactions
{
    public interface ITransactDataStorage
    {
        IDataTransaction BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted);
    }
}
