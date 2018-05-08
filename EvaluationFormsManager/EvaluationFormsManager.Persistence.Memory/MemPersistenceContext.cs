using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EvaluationFormsManager.Persistence.Memory
{
    public class MemPersistenceContext : IPersistenceContext
    {
        public ICriteriaRepository GetCriteriaRepository()
        {
            throw new NotImplementedException();
        }

        public IFormRepository GetEvaluationRepository()
        {
            throw new NotImplementedException();
        }

        public ISectionRepository GetSectionRepository()
        {
            throw new NotImplementedException();
        }

        public void InitializeContext(IServiceCollection services, IConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public void InitializeData(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
