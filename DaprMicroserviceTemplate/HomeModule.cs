using Nancy;

namespace DaprMicroServiceTemplate
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => "Hello World");

            Get("/SayHello", args => $"Hello {this.Request.Query["name"]}");

            Get("/SayHello2/{name}", args => $"Hello {args.name}");
        }
    }
}
