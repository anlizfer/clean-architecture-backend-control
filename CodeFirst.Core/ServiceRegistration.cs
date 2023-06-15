using CodeFirst.Core.Features.DocumentTypeServices;
using CodeFirst.Core.Features.SqlExample;
using CodeFirst.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeFirst.Core
{
    public static class ServiceRegistration
    {
        public static void AddCoreLayer(this IServiceCollection services)
        {

            services.AddTransient<ISqlExampleService, SqlExampleService>();
            services.AddTransient<IDocumentTypeService, DocumentTypeService>();
            //services.AddTransient<DataProtection>();            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}