using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace EvaluationFormsManager.Persistence.EF
{
    public class PersistanceContext : IPersistenceContext
    {
        private DatabaseContext databaseContext = null;
        private FormRepository formRepository = null;

        public PersistanceContext(IServiceProvider serviceProvider)
        {
            InitializeDbContext(serviceProvider);
        }

        public void InitializeDbContext(IServiceProvider serviceProvider)
        {
            if(databaseContext == null)
            {
                databaseContext = serviceProvider.GetService<DatabaseContext>();
            }

            formRepository = new FormRepository(databaseContext);
        }

        public void InitializeContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("DataConnection"),
              b => b.MigrationsAssembly("EvaluationFormsManager.Persistence.EF")));

            InitializeDbContext(services.BuildServiceProvider());
        }

        public void InitializeData(IServiceProvider serviceProvider)
        {
            // Initialize data below
        }

        public IFormRepository GetFormRepository()
        {
            return formRepository;
        }
    }
}
