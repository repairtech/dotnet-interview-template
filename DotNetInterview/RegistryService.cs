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
        private const string UninstallPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

        private const string UninstallPath32Bit = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

        private readonly ILogger<RegistryService> _logger;

        public RegistryService(ILogger<RegistryService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Check if the given software name can be found as an installed application in the Windows registry.
        /// Is case-insensitive and will return partial matches.
        /// </summary>
        /// <param name="softwareName">Name of the installed software.</param>
        /// <returns>True when it is installed.</returns>
        public bool CheckIfInstalled(string softwareName)
        {
            var uninstallers = Registry.LocalMachine.OpenSubKey(UninstallPath);
            if (uninstallers != null)
            {
                _logger.LogDebug("Checking the default uninstaller list for: {softwareName}", softwareName);
                if (UninstallerKeyContains(softwareName, uninstallers)) return true;
            }

            var uninstallers32Bit = Registry.LocalMachine.OpenSubKey(UninstallPath32Bit);
            if (uninstallers32Bit is null) return false;

            _logger.LogDebug("Checking the 32 bit uninstaller list for: {softwareName}", softwareName);
            return  UninstallerKeyContains(softwareName, uninstallers32Bit);
        }

        /// <summary>
        /// Check the uninstallers registry key to see if it contains a given software name.
        /// </summary>
        /// <param name="softwareName">The name of the software to check for.</param>
        /// <param name="uninstallers">The uninstaller key in the registry to search.</param>
        /// <returns>True when the software is found.</returns>
        private bool UninstallerKeyContains(string softwareName, RegistryKey uninstallers)
        {
            var installedKeys = uninstallers.GetSubKeyNames().Select(key =>
            {
                var uninstaller  = uninstallers.OpenSubKey(key);
                var displayName = uninstaller?.GetValue("DisplayName").ToString();
                _logger.LogTrace(displayName);
                return displayName ?? "";
            });

            return installedKeys.Any(name => name.IndexOf(softwareName, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}
