using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EvaluationFormsManager.CommonAbstractions
{
    public interface IInitializer
    {
        void InitializeContext(IServiceCollection services, IConfiguration configuration);
        void InitializeData(IServiceProvider serviceProvider);
    }
}
