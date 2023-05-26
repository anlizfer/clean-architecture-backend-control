using CodeFirst.Domain.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace CodeFirst.Web.Api.Extensions.App
{
    public static class MilddlewareAppExtension
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalErrorException>();
        }
    }
}