using AuthenticationService.Abstractions;

namespace AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        public string GetCurrentUserId()
        {
            return "userId";
        }

        public bool IsUserAuthenticated()
        {
            return true;
        }
    }
}
