using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AMC.Core.Abstractions.Quantums;
using AMC.Core.Abstractions.QuantumAdapter;

namespace Testing
{
    public sealed class PopulatorRepository
    {
        private IDictionary<Type, IPopulator<IQuant>> _dict { get; }

        public PopulatorRepository(params IPopulator<IQuant>[] populators)
        {
            _dict = populators.AsParallel().ToDictionary(sx => sx.GetType(), sy => sy);
        }

        public PopulatorRepository(IEnumerable<IPopulator<IQuant>> populators)
        {
            _dict = populators.AsParallel().ToDictionary(sx => sx.GetType(), sy => sy);
        }

        public IPopulator<IQuant> this[Type T] => _dict[T];

        public IPopulator<IQuant> GetPopulator<T>(T entity) where T : IQuant => _dict[typeof(T)];

        public bool TryGetPopulator<T>(out IPopulator<IQuant> populator) where T : IQuant
        {
            if (_dict.ContainsKey(typeof(T)))
            {
                populator = _dict[typeof(T)];
                return true;
            }
                
            populator = null;
            return false;
        }
    }
}
