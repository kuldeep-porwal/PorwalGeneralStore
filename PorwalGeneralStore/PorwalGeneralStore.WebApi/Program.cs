using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace PorwalGeneralStore.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateWebHostBuilder(args).Build();

            var logger = hostBuilder.Services.GetRequiredService<ILogger<Program>>();
            using (logger.BeginScope("RailwayEnquiry App"))
            {
                logger.LogInformation("Railway Enquiry App Started");
                hostBuilder.Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel()
            .UseIISIntegration()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                          optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .UseSerilog((hostingContext, logging) =>
            {
                logging.ReadFrom.Configuration(hostingContext.Configuration);
            })
            .UseStartup<Startup>();
    }
}
