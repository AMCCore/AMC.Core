﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.Quantums
{
    public interface IQuant
    {
        ulong Id { get; }

        uint TypeId { get; }

        QuantumTypes.IQuantumType Type { get; }

        IQuantumValueCollection Values { get; }
    }
}
