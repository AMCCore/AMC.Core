using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumBasis
{
    public class BaseQuantum
    {
        protected BaseQuantum()
        {
        }

        public BaseQuantum(QuantumTypes.BaseQuantumType QuantumType) : this()
        {
            _quantumType = QuantumType;
        }

        public ulong Id { get; private set; }

        protected QuantumTypes.BaseQuantumType _quantumType;

        public QuantumTypes.BaseQuantumType QuantumType
        {
            get
            {
                return _quantumType;
            }
        }
    }
}
