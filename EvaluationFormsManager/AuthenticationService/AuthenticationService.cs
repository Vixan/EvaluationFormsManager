using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace EvaluationFormsManager.Authentication
{
    public class AuthenticationService : Abstractions.IAuthenticationService
    {
        private HttpContext currentContext;

        public AuthenticationService(IHttpContextAccessor context)
        {
            currentContext = context?.HttpContext;
        }

        public string GetCurrentUserId()
        {
            var nameClaim = currentContext.User.Claims.Where(claim => claim.Type.Equals("name")).FirstOrDefault();

            return nameClaim.Value;
        }

        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            string returnUrl = "signin-oidc";

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var authSettings = configuration.GetSection("Authentication");

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

                    options.Authority = authSettings["Authority"];
                    options.RequireHttpsMetadata = false;
                    options.CallbackPath = PathString.FromUriComponent("/" + returnUrl);
                    options.ClientId = authSettings["ClientId"];
                    options.ClientSecret = authSettings["ClientSecret"];
                    options.ResponseType = "code id_token";

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.Scope.Add("GetEmployees");
                });
        }

        public bool IsUserAuthenticated()
        {
            return currentContext.User.Identity.IsAuthenticated;
        }
        
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseAuthentication();
        }

        public void SignOut()
        {
            currentContext?.SignOutAsync("Cookies").Wait();
            currentContext?.SignOutAsync("oidc").Wait();
        }
    }
}
