using AMC.Core.Abstractions.Quantums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumAdapter
{
    public interface IPopulatorRepository
    {
        IDictionary<Type, IPopulator<IQuant>> Repo { get; }

        IPopulator<IQuant> this[Type T] { get; }

        IPopulator<IQuant> GetPopulator<T>(T entity) where T : IQuant;

        bool TryGetPopulator<T>(out IPopulator<IQuant> populator) where T : IQuant;

        bool ContainsKey(Type T);
    }
}
