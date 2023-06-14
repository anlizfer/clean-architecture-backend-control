using CodeFirst.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CodeFirst.InfrastructureOLEDB
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IGenericRepositoryOLEDB, IGenericRepositoryOLEDB>();

            return services;
        }
    }
}