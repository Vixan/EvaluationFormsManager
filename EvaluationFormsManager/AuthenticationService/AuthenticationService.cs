using AuthenticationService.Abstractions;

namespace AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        public string GetCurrentUserId()
        {
            return "1";
        }

        public bool IsUserAuthenticated()
        {
            return true;
        }
    }
}
