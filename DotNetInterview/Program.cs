using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInterview
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            serviceCollection.AddScoped<IApiService, ApiService>();
            serviceCollection.AddScoped<IRegistryService, RegistryService>();
            serviceCollection.AddScoped<ISoftwareReporter, SoftwareReporter>();

            var container = serviceCollection.BuildServiceProvider();

            ISoftwareReporter softwareReporter = container.GetService<ISoftwareReporter>();

            await softwareReporter.ReportSoftwareInstallationStatus("Syncro");

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
