using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DaprActorTemplate
{
    public class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        public static void Main(string[] args)
        {
            var logRepo = LogManager.GetRepository(Assembly.GetEntryAssembly());
            var envstring = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            XmlConfigurator.Configure(logRepo, new FileInfo($"log4net.{envstring}.config"));
            log.Info($"Microservice $safeprojectname$ starting");
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var aspnetcoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var currentConfig = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.{aspnetcoreEnvironment}.json")
                    .Build();

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(currentConfig);
                    webBuilder.UseStartup<Startup>();
                        
                });
        }

    }
}
