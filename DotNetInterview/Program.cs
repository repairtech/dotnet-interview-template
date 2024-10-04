using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DotNetInterview
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole();
#if DEBUG
                builder.SetMinimumLevel(LogLevel.Trace);
#endif
            });

            serviceCollection.RegisterApiService();
            serviceCollection.RegisterRegistryService();
            serviceCollection.RegisterSoftwareReporter();
            var container = serviceCollection.BuildServiceProvider();

            var softwareReporter = container.GetService<ISoftwareReporter>();
            if (softwareReporter is null) return 1;
            
            try
            {
                var softwareList = args.Length == 0 ? ["Syncro"] : args;
                foreach (var softwareName in softwareList)
                {
                    await softwareReporter.ReportSoftwareInstallationStatus(softwareName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.HResult;
            }
            
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();

            return 0;
        }
    }
}
