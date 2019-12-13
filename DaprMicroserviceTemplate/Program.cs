using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;

namespace DaprMicroServiceTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext,config) =>
                    {

                        if (hostingContext.HostingEnvironment.IsEnvironment("Development"))
                        {
                            config.AddAzureAppConfiguration(opt =>
                            {
                                opt.ConnectionString = "<Azure App Configuration Connection String to be Filled in Here>";
                                opt.Use(KeyFilter.Any, "\0").Use(KeyFilter.Any, "Development");
                            });
                        }
                        else if (hostingContext.HostingEnvironment.IsEnvironment("Staging"))
                        {
                            config.AddAzureAppConfiguration(opt =>
                            {
                                opt.ConnectionString = "<Azure App Configuration Connection String to be Filled in Here>";
                                opt.Use(KeyFilter.Any, "\0").Use(KeyFilter.Any, "Staging");
                            });
                        }
                        else
                            config.AddAzureAppConfiguration(opt =>
                            {
                                opt.ConnectionString = "<Azure App Configuration Connection String to be Filled in Here>";
                                opt.Use(KeyFilter.Any, "\0").Use(KeyFilter.Any, "Production");
                            });
                    });
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {

                    })
                    .UseStartup<Startup>();
                });
    }
}
