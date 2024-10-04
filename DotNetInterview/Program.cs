using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInterview
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            serviceCollection.RegisterApiService();
            serviceCollection.RegisterRegistryService();
            serviceCollection.RegisterSoftwareReporter();
            var container = serviceCollection.BuildServiceProvider();

            var softwareReporter = container.GetService<ISoftwareReporter>();

            await softwareReporter.ReportSoftwareInstallationStatus("Syncro");

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
