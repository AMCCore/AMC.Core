using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace AMC.Core.Abstractions.Quantums
{
    internal class AQuantum : DynamicObject
    {
        public long Id { get; private set; }

        public QuantumType QuantumType { get; private set; }

        protected ValueCollectionDictionary Values { get; set; }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if(string.Equals(binder.Name, nameof(Id), StringComparison.OrdinalIgnoreCase))
            {
                result = Id;
                return true;
            }
            else if (string.Equals(binder.Name, nameof(QuantumType), StringComparison.OrdinalIgnoreCase))
            {
                result = QuantumType;
                return true;
            }

            return Values.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return base.TrySetMember(binder, value);
        }
    }
}
