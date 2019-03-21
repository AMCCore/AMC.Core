using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumModel.QuantumHistory
{
    public interface IQuantumHistoryEventCollection
    {
        ICollection<IQuantumHistoryEvent> History { get; }

        ulong CreatorId { get; }

        IQuant Creator { get; }

        DateTime DateCreation { get; }

        ulong LastUpdaterId { get; }

        IQuant LastUpdater { get; }

        DateTime DateLastUpdate { get; }
    }
}
