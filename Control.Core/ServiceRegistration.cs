using Control.Core.Features.DocumentTypeServices;
using Control.Core.Features.SqlExample;
using Control.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Control.Core
{
    public static class ServiceRegistration
    {
        public static void AddCoreLayer(this IServiceCollection services)
        {

            services.AddTransient<ISqlExampleService, SqlExampleService>();
            services.AddTransient<IDocumentTypeService, DocumentTypeService>();            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}