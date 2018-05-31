using EvaluationFormsManager.Authentication;
using EvaluationFormsManager.Authentication.Abstractions;
using EvaluationFormsManager.Core;
using EvaluationFormsManager.Core.Shared;
using EvaluationFormsManager.Persistence;
using EvaluationFormsManager.Persistence.EF;
using EvaluationFormsManager.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaluationFormsManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IAuthenticationService AuthenticationService { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add persistance
            services.AddScoped<IPersistenceContext, PersistanceContext>();
            var dataService = services.BuildServiceProvider().GetService<IPersistenceContext>();
            dataService?.InitializeContext(services, Configuration);

            // Add business
            services.AddScoped<IFormService, FormService>();

            // Add Authentication
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddMvc();

            // Initialize Authentication service
            AuthenticationService = services.BuildServiceProvider().GetService<IAuthenticationService>();
            AuthenticationService?.Initialize(services, Configuration);

            // Adds a default in-memory implementation of IDistributedCache
            services.AddDistributedMemoryCache(); 
            services.AddSession(sessionOptions => 
            {
                sessionOptions.Cookie.Name = "EvaluationFormsManager.Session";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            AuthenticationService?.Configure(app);

            app.UseStaticFiles();

            // IMPORTANT: This session call MUST go before UseMvc()
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Forms}/{action=Index}");
            });
        }
    }
}
