﻿using Microsoft.Extensions.DependencyInjection;
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

            serviceCollection.RegisterApiService();
            serviceCollection.RegisterRegistryService();
            serviceCollection.RegisterSoftwareReporter();
            var container = serviceCollection.BuildServiceProvider();


            // TODO: Retrieve an instance of SoftwareReporter from the
            // dependency injection container.

            // TODO: Call ReportSoftwareInstallationStatus method, using "Syncro"
            // as the software name.

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
