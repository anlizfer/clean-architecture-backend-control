using CodeFirst.Core.Features.Pagination;
using CodeFirst.Domain.Interfaces;
using CodeFirst.Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeFirst.Web.Api.Extensions.Service
{
    public static class PaginationServiceExtension
    {
        public static void AddPaginationExtension(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                IHttpContextAccessor accessor = o.GetRequiredService<IHttpContextAccessor>();
                HttpRequest request = accessor.HttpContext.Request;
                string uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            services.Configure<PaginationOptionsSetting>(options => Configuration.GetSection("Pagination").Bind(options));
        }
    }
}