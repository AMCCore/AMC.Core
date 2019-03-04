using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider.Helpers
{
    public class ObjectNotFoundException : Exception
    {
        private const string _template = "Object with identificator '{0}' not found";

        public ObjectNotFoundException(long Id) : base(string.Format(_template, Id)) { }
    }
}
