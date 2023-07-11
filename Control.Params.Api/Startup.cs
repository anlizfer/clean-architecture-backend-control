using Control.Core;
using Control.Infrastructure;
using Control.Infrastructure.Settings;
using Control.InfrastructureOLEDB;
using Control.Params.api.Extensions.App;
using Control.Params.api.Extensions.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

namespace Control.Params.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Limpia el mapeo por default de los claim.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Infraestructura
            services.AddDbContexts(Configuration);            
            services.AddRepository();
            services.AddRepositoryOLEDB();

            //Core
            services.AddCoreLayer();

            //paginacion
            services.AddPaginationExtension(Configuration);

            //Configuracion Swagger
            services.AddSwaggerExtension();

            //Configuracion acceso controlador
            services.AddControllerExtension();

            //Seguridad y procteecion de datos
            services.AddAuthenticationExtension(Configuration);
            services.AddAuthorizationExtension();
            services.AddCorsExtension();
            services.AddDataProtection();

            //Salud de los servicios
            services.AddHealthChecks()
                .AddDbContextCheck<ControlContext>("Sql");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerExtension();
            }

            app.UseHealthChecks("/health");
            //Exception Handler
            //HSTS
            //HTTPS Redirection
            app.UseHttpsRedirection();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            //Static Files
            //--app.UseStaticFiles();

            //UseCookie
            //--app.UseCookiePolicy();

            //Routing
            app.UseRouting();
            // Request Localization
            //--app.UseRequestLocalization();
            //CORS
            app.UseCors();

            //Authorization
            app.UseAuthorization();
            //--app.UseSession();
            //--app.UseResponseCompression();
            //--app.UseResponseCaching();

            //Personalizadas
            app.UseErrorHandlingMiddleware();
            app.UseSerilogRequestLogging();

            //Endpoint Configuration
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHealthChecks("/health");
                }
            );
        }
    }
}