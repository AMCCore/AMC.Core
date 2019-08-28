using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.Security
{
    public interface IWebAuthenticationModule
    {
        void Logout();

        bool Authenticate(ILoginModel loginModel);

        bool AuthenticateExternal(Microsoft.AspNet.Identity.Owin.ExternalLoginInfo LoginInfo);
    }
}
