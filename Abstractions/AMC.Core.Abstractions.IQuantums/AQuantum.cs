using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace AMC.Core.Abstractions.Quantums
{
    public class AQuantum : DynamicObject
    {
        public long Id { get; private set; }

        public QuantumType QuantumType { get; private set; }

        internal ValueCollectionDictionary Values { get; set; }

        public DateTime? DateCreated { get; private set; }

        protected long? CreatorId { get; set; }

        public DateTime? DateUpdated { get; private set; }

        protected long? UpdaterId { get; set; }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if(string.Equals(binder.Name, nameof(Id), StringComparison.OrdinalIgnoreCase))
            {
                result = Id;
                return true;
            }
            if (string.Equals(binder.Name, nameof(QuantumType), StringComparison.OrdinalIgnoreCase))
            {
                result = QuantumType;
                return true;
            }
            if (string.Equals(binder.Name, nameof(DateCreated), StringComparison.OrdinalIgnoreCase))
            {
                result = DateCreated;
                return DateCreated.HasValue;
            }
            if (string.Equals(binder.Name, nameof(DateUpdated), StringComparison.OrdinalIgnoreCase))
            {
                result = DateUpdated;
                return DateUpdated.HasValue;
            }

            return Values.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return base.TrySetMember(binder, value);
        }
    }
}
