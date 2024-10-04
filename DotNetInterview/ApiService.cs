using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DotNetInterview
{
    public interface IApiService
    {
        Task<int> SendInstalledSoftware(string softwareName, bool isInstalled);
    }

    public class ApiService(ILogger<ApiService> logger) : IApiService
    {
        public async Task<int> SendInstalledSoftware(string softwareName, bool isInstalled)
        {
            logger.LogInformation("Sending installation status for {softwareName}.  Installed: {isInstalled}",
                softwareName,
                isInstalled);

            // Simulate an HTTP request.  Normally you'd be awaiting an async
            // method on the HttpClient.
            var statusCode = await Task.Run(() =>
            {
                return 200;
            });

            return statusCode;
        }
    }
}
