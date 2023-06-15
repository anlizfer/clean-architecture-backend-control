using Control.Core.Interfaces.Repositories;
using Control.InfrastructureOLEDB.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Control.InfrastructureOLEDB
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