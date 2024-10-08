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

            // TODO: Register ApiService, RegistryService, and SoftwareReporter
            // in the ServiceCollection.

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
