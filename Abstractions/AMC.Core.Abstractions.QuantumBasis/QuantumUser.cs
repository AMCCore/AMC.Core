using System;
using System.Collections.Generic;
using System.Text;
using AMC.Core.Abstractions.QuantumBasis.QuantumTypes;

namespace AMC.Core.Abstractions.QuantumBasis
{
    public class QuantumUser : BaseQuantum
    {
        public QuantumUser() : base(new QuantumTypes.QuantumUser())
        {
        }
    }
}
