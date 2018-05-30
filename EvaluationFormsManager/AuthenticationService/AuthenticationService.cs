using EvaluationFormsManager.Authentication.Abstractions;

namespace EvaluationFormsManager.Authentication
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
