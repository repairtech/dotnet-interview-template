using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DotNetInterview
{
    public interface ISoftwareReporter
    {
        Task ReportSoftwareInstallationStatus(string softwareName);
    }

    /// <summary>
    /// Reports on the installed software.
    /// </summary>
    public class SoftwareReporter(IRegistryService registryService, IApiService apiService, ILogger<SoftwareReporter> logger) : ISoftwareReporter
    {
        /// <summary>
        /// Check if a given software is installed and report the status to the Api.
        /// </summary>
        /// <param name="softwareName">The name of the software to check.</param>
        public Task ReportSoftwareInstallationStatus(string? softwareName)
        {
            if (string.IsNullOrWhiteSpace(softwareName)) throw new ArgumentNullException(nameof(softwareName));
            
            var isInstalled = registryService.CheckIfInstalled(softwareName);
            var status = isInstalled ? "is" : "is not";
            logger.LogDebug("{softwareName} {status} installed.", softwareName, status);
            apiService.SendInstalledSoftware(softwareName, isInstalled);
            
            return Task.CompletedTask;
        }
    }
}
