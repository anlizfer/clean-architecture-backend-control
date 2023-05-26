using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace CodeFirst.Web.Api
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                  .AddJsonFile(
                                                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json"
                                                    , optional: true
                                                    , reloadOnChange: true
                                                               )
                                                  .Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            try
            {
                Log.Warning("Inicio de alojamiento web");
                CreateHostBuilder(args).Build().Run();
                Log.Warning("Apagando de alojamiento web");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "El host terminó inesperadamente");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}