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

        // TODO: Finish implementing this class so the unit tests
        // in SoftwareReporterTests are passing.
        public SoftwareReporter(IRegistryService registryService, IApiService apiService, ILogger<SoftwareReporter> logger)
        {
            _registryService = registryService;
            _apiService = apiService;
            _logger = logger;
        }

        public Task ReportSoftwareInstallationStatus(string softwareName)
        {
            throw new NotImplementedException();
        }
    }
}
