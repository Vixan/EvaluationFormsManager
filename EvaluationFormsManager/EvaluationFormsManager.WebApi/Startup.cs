﻿using EvaluationFormsManager.Core;
using EvaluationFormsManager.Persistence;
using EvaluationFormsManager.Persistence.EF;
using EvaluationFormsManager.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EvaluationFormsManager.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add persistance
            services.AddScoped<IPersistenceContext, PersistanceContext>();
            var dataService = services.BuildServiceProvider().GetService<IPersistenceContext>();
            dataService.InitializeContext(services, Configuration);

            // Add business
            services.AddScoped<IFormService, FormService>();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
