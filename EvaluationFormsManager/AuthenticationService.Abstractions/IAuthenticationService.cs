using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaluationFormsManager.Authentication.Abstractions
{
    public interface IAuthenticationService
    {
        string GetCurrentUserId();
        bool IsUserAuthenticated();
        void Initialize(IServiceCollection services, IConfiguration configuration);
        void Configure(IApplicationBuilder applicationBuilder);
        void SignOut();
    }
}
