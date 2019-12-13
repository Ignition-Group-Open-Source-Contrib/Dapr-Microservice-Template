using Nancy.TinyIoc;
using Nancy;
using Microsoft.Extensions.Configuration;

namespace DaprMicroServiceTemplate.Bootstrap
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly IConfiguration configuration;
        public Bootstrapper(IConfiguration configuration)
        {
            this.configuration = configuration;

        }
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            //container.Register<IAzureStorage, AzureStorage>().AsSingleton();
            container.Register(this.configuration);
        }

    }
}