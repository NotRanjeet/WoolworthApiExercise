using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Woolworth.WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                //Make sure we flush the logs before application is terminated at startup
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>();
                });
    }
}
