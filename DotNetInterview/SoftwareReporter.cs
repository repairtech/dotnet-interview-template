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
        private IRegistryService _registryService;
        private IApiService _apiService;
        private ILogger<SoftwareReporter> _logger;

        public SoftwareReporter(IRegistryService registryService, IApiService apiService, ILogger<SoftwareReporter> logger)
        {
            _registryService = registryService;
            _apiService = apiService;
            _logger = logger;
        }

        public async Task ReportSoftwareInstallationStatus(string softwareName)
        {
            if (String.IsNullOrWhiteSpace(softwareName)) throw new ArgumentNullException();

            bool isSoftwareInstalled = _registryService.CheckIfInstalled(softwareName);
            int statusCode = 0;
            try
            {
                statusCode = await _apiService.SendInstalledSoftware(softwareName, isSoftwareInstalled);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception in SoftwareReporter.ReportSoftwareInstallationStatus for {softwareName}.", softwareName);
            }

            // we could also do more depending on the status code in terms of logging or exception handling/retries
        }
    }
}
