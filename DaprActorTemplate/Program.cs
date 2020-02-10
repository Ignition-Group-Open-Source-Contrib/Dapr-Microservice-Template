using System;
using Dapr.Actors.AspNetCore;
using Dapr.Actors.Runtime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DaprActorTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                    webBuilder.UseStartup<Startup>()
                        
                        .UseActors(actorRuntime =>
                        {

                            actorRuntime.RegisterActor<Actor.SampleActor>(information =>
                            {
                                return new ActorService(information,
                                    (service, id) =>
                                    {
                                        
                                        var configSetting1 = "configSetting1";
                                        var configSetting2 = "configSetting2";
                                        return new Actor.SampleActor(service, id, configSetting1, configSetting2);
                                    });
                            });
                        });
                });
        }

    }
}
