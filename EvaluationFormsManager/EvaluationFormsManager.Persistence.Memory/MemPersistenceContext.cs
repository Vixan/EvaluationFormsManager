using EvaluationFormsManager.Domain;
using IdentityServer.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace EvaluationFormsManager.Persistence.Memory
{
    public class MemPersistenceContext : IPersistenceContext
    {
        MemFormRepository formRepository = new MemFormRepository();
        MemSectionRepository sectionRepository = new MemSectionRepository();
        MemCriteriaRepository criteriaRepository = new MemCriteriaRepository();

        public ICriteriaRepository GetCriteriaRepository()
        {
            return criteriaRepository;
        }

        public IFormRepository GetFormRepository()
        {
            return formRepository;
        }

        public ISectionRepository GetSectionRepository()
        {
            return sectionRepository;
        }

        public void InitializeContext(IServiceCollection services, IConfiguration configuration)
        {
            // No InitializeContext method implementation for in-memory persistence.
        }

        public void InitializeData(IServiceProvider serviceProvider)
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Active = true,
                    Name = "John Doe",
                    Username = "JohnnyD"
                },
                new Employee
                {
                    Id = 2,
                    Active = true,
                    Name = "Brass Len",
                    Username = "LennyBrass"
                }
            };
            List<Importance> importances = new List<Importance>
            {
                new Importance
                {
                    Id = 1,
                    Name = "Not important",
                    Level = 1
                },
                new Importance
                {
                    Id = 2,
                    Name = "Regular",
                    Level = 2
                },
                new Importance
                {
                    Id = 3,
                    Name = "Important",
                    Level = 3
                },
                new Importance
                {
                    Id = 4,
                    Name = "Very important",
                    Level = 4
                }
            };
            List<EvaluationScaleOption> evaluationScaleOptions = new List<EvaluationScaleOption>
            {
                new EvaluationScaleOption
                {
                    Id = 1,
                    Name = "Like",
                    Description = "Caesar says yes.",
                    Value = 1
                },
                new EvaluationScaleOption
                {
                    Id = 2,
                    Name = "So-so",
                    Description = "Caesar says nothing.",
                    Value = 2
                },
                new EvaluationScaleOption
                {
                    Id = 3,
                    Name = "Dislike",
                    Description = "You're dead.",
                    Value = 3
                }
            };
            List<EvaluationScale> evaluationScales = new List<EvaluationScale>
            {
                new EvaluationScale
                {
                    Id = 1,
                    Name = "Caesar Scale",
                    Description = " Cu mel veri brute voluptua, ex dicit definitionem signiferumque eam.",
                    EvaluationScaleOptions = evaluationScaleOptions
                }
            };
            List<Criteria> criteria = new List<Criteria>
            {
                new Criteria
                {
                    Id = 1,
                    Name = "Naming stuff",
                    CreatedBy = employees.Find(employee => employee.Name == "John Doe").Id,
                    ModifiedBy = employees.Find(employee => employee.Name == "John Doe").Id,
                    ModifiedDate = new DateTime(2017, 4, 21)
                }
            };
            List<Section> sections = new List<Section>
            {
                new Section
                {
                    Id = 1,
                    Name = "Software Engineering",
                    Description = "Cu ipsum oratio eum, ne quem fierent eum, meis mutat in vel. Te eos equidem necessitatibus, vim quem vidit errem ut.",
                    CreatedBy = employees.Find(employee => employee.Name == "John Doe").Id,
                    ModifiedBy = employees.Find(employee => employee.Name == "John Doe").Id,
                    ModifiedDate = new DateTime(2017, 5, 28),
                    EvaluationScale = evaluationScales.Find(scale => scale.Name == "Caesar Scale"),
                    Criteria = criteria.FindAll(crit => crit.Name == "Naming stuff")
                }
            };
            List<Form> forms = new List<Form>
            {
                new Form
                {
                    Id = 1,
                    Name = "Core Technical .NET",
                    Description = "Lorem ipsum dolor sit amet, cum at vide detraxit, solum audire pro eu, in usu disputando dissentiet. Ad duo vide nostro, eos iusto legere officiis te, cum cu putant deleniti comprehensam.",
                    CreatedBy = employees.Find(employee => employee.Name == "John Doe").Id,
                    ModifiedBy = employees.Find(employee => employee.Name == "John Doe").Id,
                    CreatedDate = new DateTime(2017, 5, 27),
                    ModifiedDate = new DateTime(2017, 5, 28),
                    Importance = importances.Find(importance => importance.Level == 3),
                    Sections = sections.FindAll(section => section.Name == "Software Engineering"),
                    Status = true
                }
            };

            criteria.ForEach(criterion => criteriaRepository.Add(criterion));
            sections.ForEach(section => sectionRepository.Add(section));
            forms.ForEach(form => formRepository.Add(form));
        }
    }
}
