using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInterview
{
    public interface ISoftwareReporter
    {
        Task ReportSoftwareInstallationStatus(string softwareName);
    }

    public class SoftwareReporter : ISoftwareReporter
    {
        private readonly IRegistryService _registryService;
        private readonly IApiService _apiService;
        private readonly ILogger<SoftwareReporter> _logger;

        /// <summary>
        /// Reports on the installed software.
        /// </summary>
        public SoftwareReporter(IRegistryService registryService, IApiService apiService, ILogger<SoftwareReporter> logger)
        {
            _registryService = registryService;
            _apiService = apiService;
            _logger = logger;
        }

        /// <summary>
        /// Check if a given software is installed and report the status to the Api.
        /// </summary>
        /// <param name="softwareName">The name of the software to check.</param>
        public Task ReportSoftwareInstallationStatus(string? softwareName)
        {
            if (string.IsNullOrWhiteSpace(softwareName)) throw new ArgumentNullException(nameof(softwareName));
            
            var isInstalled = _registryService.CheckIfInstalled(softwareName);
            var status = isInstalled ? "is" : "is not";
            _logger.LogDebug("{softwareName} {status} installed.", softwareName, status);
            _apiService.SendInstalledSoftware(softwareName, isInstalled);
            
            return Task.CompletedTask;
        }
    }
}
