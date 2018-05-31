using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using EvaluationFormsManager.Domain;
using System.Linq;

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

            databaseContext.Database.Migrate();

            formRepository = new FormRepository(databaseContext);
        }

        public void InitializeContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("DataConnection"),
              b => b.MigrationsAssembly("EvaluationFormsManager.Persistence.EF")));

            InitializeDbContext(services.BuildServiceProvider());
        }

        private void InitializeDatabaseData()
        {
            List<Importance> importances = new List<Importance>
            {
                new Importance
                {
                    Name = "Important",
                    Level = 1
                },
                new Importance
                {
                    Name = "Not important",
                    Level = 2
                }
            };
            List<Importance> existingImportances = formRepository.GetImportances().ToList();
            foreach(var importance in importances)
            {
                if (existingImportances != null)
                    if (existingImportances.Exists(imp => imp.Name == importance.Name))
                        continue;

                formRepository.AddImportance(importance);
            }

            List<Status> statuses = new List<Status>
            {
                new Status
                {
                    Name = "Enabled"
                },

                new Status
                {
                    Name = "Disabled"
                }
            };
            List<Status> existingStatuses = formRepository.GetStatuses().ToList();
            foreach(var status in statuses)
            {
                if (existingStatuses != null)
                    if (existingStatuses.Exists(sts => sts.Name == status.Name))
                        continue;

                formRepository.AddStatus(status);
            }

            databaseContext.SaveChanges();
        }

        public void InitializeData(IServiceProvider serviceProvider)
        {
            InitializeDatabaseData();
        }

        public IFormRepository GetFormRepository()
        {
            return formRepository;
        }
    }
}
