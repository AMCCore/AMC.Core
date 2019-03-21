using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider.Transactions
{
    public class TransactionStateException : Exception
    {
        private const string _template = "Can not '{0}' transaction in state other then '{1}'";

        public TransactionStateException(string ActionName, string NeedSatateName) : base(string.Format(_template, ActionName, NeedSatateName)) { }
    }
}
