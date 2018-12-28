using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.Quantums.QuantumHistory
{
    public interface IQuantumHistoryEvent
    {
        ulong Id { get; }

        DateTime Date { get; }

        ulong AuthorId { get; }

        IQuant Author { get; }

        string QuantSplit { get; }
    }
}
