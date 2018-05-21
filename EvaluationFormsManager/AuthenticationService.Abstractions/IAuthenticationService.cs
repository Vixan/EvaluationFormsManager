namespace AuthenticationService.Abstractions
{
    public interface IAuthenticationService
    {
        string GetCurrentUserId();
        bool IsUserAuthenticated();
    }
}
