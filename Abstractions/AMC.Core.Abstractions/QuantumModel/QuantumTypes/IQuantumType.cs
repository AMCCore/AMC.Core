using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumModel.QuantumTypes
{
    public interface IQuantumType
    {
        uint Id { get; }

        uint? SpecialId { get; }

        string Code { get; }

        bool IsSpecial { get; }

        IList<QuantumFields.IQuantumField> Fields { get; }
    }
}
