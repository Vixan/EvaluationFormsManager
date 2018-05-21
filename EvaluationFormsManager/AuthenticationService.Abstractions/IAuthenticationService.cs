using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationService.Abstractions
{
    public interface IAuthenticationService
    {
        string GetCurrentUserId();
    }
}
