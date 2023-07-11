using Control.Core.Interfaces.Repositories;
using Control.Infrastructure.Repositories;
using Control.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Control.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ControlContext>(options =>
            options.UseSqlServer(connectionString: configuration.GetConnectionString("Control")));

            return services;
        }

        
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}