using Control.Domain.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Control.Params.api.Extensions.App
{
    public static class MilddlewareAppExtension
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalErrorException>();
        }
    }
}