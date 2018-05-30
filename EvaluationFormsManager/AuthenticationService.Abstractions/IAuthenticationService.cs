using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaluationFormsManager.Authentication.Abstractions
{
    public interface IAuthenticationService
    {
        string GetCurrentUserId();
        bool IsUserAuthenticated();
        void Initialize(IServiceCollection services, IConfiguration configuration);
    }
}
