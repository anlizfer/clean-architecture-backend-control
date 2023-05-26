using CodeFirst.Core.Features.Account;
using CodeFirst.Core.Features.CourseServices;
using CodeFirst.Core.Features.DataProtectionService;
using CodeFirst.Core.Features.InscriptionServices;
using CodeFirst.Core.Features.StudentServices;
using CodeFirst.Core.Interfaces.Services;
using CodeFirst.Domain.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeFirst.Core
{
    public static class ServiceRegistration
    {
        public static void AddCoreLayer(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentServices>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IInscriptionService, InscriptionService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<DataProtection>();
            services.AddTransient<IDataProtectionService, DataProtectionService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}