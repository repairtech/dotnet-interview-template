using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;

namespace DotNetInterview
{
    public interface IRegistryService
    {
        bool CheckIfInstalled(string softwareName);
    }

    public class RegistryService(ILogger<RegistryService> logger) : IRegistryService
    {
        private const string UninstallPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

        private const string UninstallPath32Bit = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

        /// <summary>
        /// Check if the given software name can be found as an installed application in the Windows registry.
        /// Is case-insensitive and will return partial matches.
        /// </summary>
        /// <param name="softwareName">Name of the installed software.</param>
        /// <returns>True when it is installed.</returns>
        public bool CheckIfInstalled(string softwareName)
        {
            using var uninstallers = Registry.LocalMachine.OpenSubKey(UninstallPath);
            if (uninstallers != null)
            {
                logger.LogDebug("Checking the default uninstaller list for: {softwareName}", softwareName);
                if (UninstallerKeyContains(softwareName, uninstallers)) return true;
            }

            using var uninstallers32Bit = Registry.LocalMachine.OpenSubKey(UninstallPath32Bit);
            if (uninstallers32Bit is null) return false;

            logger.LogDebug("Checking the 32 bit uninstaller list for: {softwareName}", softwareName);
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
                using var uninstaller  = uninstallers.OpenSubKey(key);
                
                // Use the Display Name or fallback to the Key, which sometimes contains the name of the software.
                var displayName = uninstaller?.GetValue("DisplayName")?.ToString() ?? 
                                  uninstaller?.Name.Substring(uninstaller.Name.LastIndexOf('\\') + 1);

                logger.LogTrace(displayName);
                return displayName ?? "";
            });

            return installedKeys.Any(name => name.IndexOf(softwareName, StringComparison.InvariantCultureIgnoreCase) >= 0);
        }
    }
}
