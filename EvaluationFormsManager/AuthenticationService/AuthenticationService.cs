using AuthenticationService.Abstractions;
using System;

namespace AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        public string GetCurrentUserId()
        {
            return "abcd";
        }
    }
}
