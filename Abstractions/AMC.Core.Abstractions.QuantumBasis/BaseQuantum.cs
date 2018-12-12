using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumBasis
{
    public abstract class BaseQuantum
    {
        protected BaseQuantum()
        {
        }

        public BaseQuantum(QuantumTypes.BaseQuantumType QuantumType) : this()
        {
            _quantumType = QuantumType;
        }

        public ulong Id { get; private set; }

        public QuantumTypes.BaseQuantumType QuantumType
        {
            get
            {
                return _quantumType;
            }
        }
        protected QuantumTypes.BaseQuantumType _quantumType;

        public QuantumValueCollection Valuse { get; private set; }

        public IReadOnlyCollection<QuantumHistory.QuantumHistoryEvent> Events { get; private set; }
    }
}
