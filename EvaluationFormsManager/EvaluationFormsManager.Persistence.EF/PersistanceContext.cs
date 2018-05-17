using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using EvaluationFormsManager.Domain;

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

        private void InitializeDatabaseData()
        {
            #region initialData
            List<Importance> importances = new List<Importance>
            {
                new Importance
                {
                    Name = "Not important",
                    Level = 1
                },
                new Importance
                {
                    Name = "Regular",
                    Level = 2
                },
                new Importance
                {
                    Name = "Important",
                    Level = 3
                },
                new Importance
                {
                    Name = "Very important",
                    Level = 4
                }
            };
            List<Criteria> criteria = new List<Criteria>
            {
                new Criteria
                {
                    Name = "Naming stuff",
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    ModifiedDate = new DateTime(2017, 4, 21)
                }
            };
            List<Section> sections = new List<Section>
            {
                new Section
                {
                    Name = "Software Engineering",
                    Description = "Cu ipsum oratio eum, ne quem fierent eum, meis mutat in vel. Te eos equidem necessitatibus, vim quem vidit errem ut.",
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    ModifiedDate = new DateTime(2017, 5, 28),
                    EvaluationScale = EvaluationScale.Agreement,
                    Criteria = criteria.FindAll(crit => crit.Name == "Naming stuff")
                }
            };
            List<Form> forms = new List<Form>
            {
                new Form
                {
                    Name = "Core Technical .NET",
                    Description = "Lorem ipsum dolor sit amet, cum at vide detraxit, solum audire pro eu, in usu disputando dissentiet. Ad duo vide nostro, eos iusto legere officiis te, cum cu putant deleniti comprehensam.",
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CreatedDate = new DateTime(2017, 5, 27),
                    ModifiedDate = new DateTime(2017, 5, 28),
                    Importance = importances.Find(importance => importance.Level == 3),
                    Sections = sections.FindAll(section => section.Name == "Software Engineering"),
                    Status = new Status
                    {
                        Name = "Enabled"
                    }
                },
                 new Form
                {
                    Name = "Team Lead Evaluation",
                    Description = "Lorem ipsum dolor sit amet, cum at vide detraxit, solum audire pro eu, in usu disputando dissentiet. Ad duo vide nostro, eos iusto legere officiis te, cum cu putant deleniti comprehensam.",
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CreatedDate = new DateTime(2018, 1, 10),
                    ModifiedDate = new DateTime(2018, 1, 10),
                    Importance = importances.Find(importance => importance.Level == 1),
                    Sections = sections.FindAll(section => section.Name == "Software Engineering"),
                    Status = new Status
                    {
                        Name = "Disabled"
                    }
                }
            };
            #endregion

            forms.ForEach(form => databaseContext.Forms.Add(form));
            databaseContext.SaveChanges();
        }

        public void InitializeData(IServiceProvider serviceProvider)
        {
            //InitializeDatabaseData();
        }

        public IFormRepository GetFormRepository()
        {
            return formRepository;
        }
    }
}
