using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Alnudaar_ChildControlApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);

            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Ctrl+C pressed, shutting down gracefully...");
                e.Cancel = true; // Prevent immediate exit
            };

            hostBuilder.Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<DatabaseService>();
                    services.AddHostedService<Worker>();
                });
    }
}