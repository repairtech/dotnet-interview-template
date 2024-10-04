using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetInterview
{
    /// <summary>
    /// Methods for registering services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register the <see cref="ApiService"/>
        /// </summary>
        public static IServiceCollection RegisterApiService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IApiService>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<ApiService>>();
                return new ApiService(logger);
            });

            return serviceCollection;
        }

        /// <summary>
        /// Register the <see cref="RegistryService" />.
        /// </summary>
        public static IServiceCollection RegisterRegistryService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IRegistryService>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RegistryService>>();
                return new RegistryService(logger);
            });

            return serviceCollection;
        }

        /// <summary>
        /// Register the <see cref="SoftwareReporter"/>
        /// </summary>
        public static IServiceCollection RegisterSoftwareReporter(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISoftwareReporter>(sp =>
            {
                var apiService = sp.GetRequiredService<IApiService>();
                var registryService = sp.GetRequiredService<IRegistryService>();
                var logger = sp.GetRequiredService<ILogger<SoftwareReporter>>();
                return new SoftwareReporter(registryService, apiService, logger);
            });

            return serviceCollection;
        }
    }
}
