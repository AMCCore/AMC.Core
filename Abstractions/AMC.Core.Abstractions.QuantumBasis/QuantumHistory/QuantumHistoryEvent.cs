using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumBasis.QuantumHistory
{
    public sealed class QuantumHistoryEvent : AHistoryEvent
    {
        public ulong QuantumId { get; private set; }
    }
}
