using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumBasis.QuantumHistory
{
    public sealed class QuantumTypeHistoryEvent : AHistoryEvent
    {
        public uint QuantumTypeId { get; private set; }

        public string QuantumTypeSnapshoot { get; private set; }
    }
}
