using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace AMC.Core.Abstractions.Security
{
    public interface IUserBase : IIdentity, IUser
    {

    }
}
