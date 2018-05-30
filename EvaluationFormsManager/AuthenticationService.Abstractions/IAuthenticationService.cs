namespace EvaluationFormsManager.Authentication.Abstractions
{
    public interface IAuthenticationService
    {
        string GetCurrentUserId();
        bool IsUserAuthenticated();
    }
}
