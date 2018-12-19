using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumBasis.QuantumHistory
{
    public abstract class AHistoryEvent
    {
        public ulong Id { get; private set; }

        public DateTime EventDate { get; private set; }

        public ulong UserId { get; private set; }
    }
}
