using Microsoft.AspNetCore.Builder;

namespace CodeFirst.Web.Api.Extensions.App
{
    public static class SwaggerAppExtension
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitecture");
            });
        }
    }
}