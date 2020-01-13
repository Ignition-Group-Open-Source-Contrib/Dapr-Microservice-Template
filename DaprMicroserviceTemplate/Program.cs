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
                 webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     config.AddAzureAppConfiguration(opt =>
                     {
                         opt.ConnectionString = "<insert azure app configuration connection string here>";
                         opt.Use(KeyFilter.Any, hostingContext.HostingEnvironment.EnvironmentName);
                     });
                 });
                 webBuilder.ConfigureKestrel(serverOptions =>
                 {



                 })
                 .UseStartup<Startup>();
             });
    }
}
