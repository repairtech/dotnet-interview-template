using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInterview
{
    public interface IRegistryService
    {
        bool CheckIfInstalled(string softwareName);
    }

    public class RegistryService : IRegistryService
    {
        private readonly string REGISTRY_SUBKEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        private readonly string REGISTRY_SUBKEY_X64 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

        private ILogger<RegistryService> _logger;

        public RegistryService(ILogger<RegistryService> logger)
        {
            _logger = logger;
        }

        public bool CheckIfInstalled(string softwareName)
        {
            bool foundAndIsInstalled = false;
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(REGISTRY_SUBKEY) ??
                      Registry.LocalMachine.OpenSubKey(
                          REGISTRY_SUBKEY_X64);

                if (registryKey == null) return false;

                foundAndIsInstalled = registryKey.GetSubKeyNames()
                    .Select(keyName => registryKey.OpenSubKey(keyName))
                    .Select(subkey => subkey.GetValue("DisplayName") as string)
                    .Any(displayName => displayName != null && displayName.ToLower().Contains(softwareName.ToLower()));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception in RegistryService.CheckIfInstalled for softwareName={softwareName}.", softwareName);
            }

            return foundAndIsInstalled;
        }
    }
}
