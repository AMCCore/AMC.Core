using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumAdapter
{
    public interface IPopulatableQuantum<T> : Quantums.IQuant where T : Quantums.IQuant
    {
        IPopulator<T> GetPopulator();
    }
}
