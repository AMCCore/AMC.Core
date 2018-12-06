using System;
using System.Collections.Generic;
using System.Text;
using AMC.Core.Abstractions.QuantumBasis.QuantumFields;

namespace AMC.Core.Abstractions.QuantumBasis
{
    public class QuantumValueCollection
    {
        private class MyClassSpecialComparer : IEqualityComparer<QuantumField>
        {
            public bool Equals(QuantumField x, QuantumField y)
            {
                return x.Id == y.Id && string.Equals(x.Code, y.Code, StringComparison.OrdinalIgnoreCase);
            }

            public int GetHashCode(QuantumField obj)
            {
                return obj.Id.GetHashCode() + obj.Code.GetHashCode();
            }
        }

        public readonly IDictionary<QuantumField, object> dict = new Dictionary<QuantumField, object>(new MyClassSpecialComparer());


    }
}
