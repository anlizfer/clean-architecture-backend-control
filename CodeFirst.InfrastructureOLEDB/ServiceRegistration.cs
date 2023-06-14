using CodeFirst.Core.Interfaces.Repositories;
using CodeFirst.InfrastructureOLEDB.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CodeFirst.InfrastructureOLEDB
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddRepositoryOLEDB(this IServiceCollection services)
        {
            services.AddTransient<IGenericRepositoryOLEDB, GenericRepositoryOLEDB>();

            return services;
        }
    }
}