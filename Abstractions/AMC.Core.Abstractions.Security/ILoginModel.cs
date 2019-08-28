using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.Security
{
    public interface ILoginModel
    {
        string Login { get; }

        string Password { get; }

        bool Remeber { get; }
    }
}
