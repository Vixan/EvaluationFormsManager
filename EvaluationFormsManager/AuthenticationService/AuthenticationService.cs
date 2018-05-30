using EvaluationFormsManager.Authentication.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace EvaluationFormsManager.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public string GetCurrentUserId()
        {
            return "userId";
        }

        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            string returnUrl = "signin-oidc";

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";

                    options.Authority = configuration.GetSection("Authentication")["Authority"];
                    options.RequireHttpsMetadata = false;
                    options.CallbackPath = PathString.FromUriComponent("/" + returnUrl);
                    options.ClientId = configuration.GetSection("Authentication")["ClientId"];
                    options.ClientSecret = configuration.GetSection("Authentication")["ClientSecret"];
                    options.ResponseType = "code id_token";

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.Scope.Add("GetEmployees");
                });
        }

        public bool IsUserAuthenticated()
        {
            return true;
        }
    }
}
