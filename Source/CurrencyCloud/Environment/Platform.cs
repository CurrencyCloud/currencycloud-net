using Microsoft.Win32;
using System;
using System.Reflection;

namespace CurrencyCloud.Environment
{
    static class Platform
    {
        static Platform()
        {
            Type monoRuntime = Type.GetType("Mono.Runtime");
            if (monoRuntime != null)
            {
                MethodInfo displayName = monoRuntime.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);
                if (displayName != null)
                {
                    Version = displayName.Invoke(null, null).ToString();
                }
            }
            else
            {
                using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
                {
                    string versionNumber = "4.0 or earlier";

                    object releaseValue = ndpKey.GetValue("Release");
                    if(releaseValue != null)
                    {
                        int releaseNumber = Convert.ToInt32(releaseValue);
                        if (releaseNumber >= 393295)
                        {
                            versionNumber = "4.6 or later";
                        }
                        else if (releaseNumber >= 379893)
                        {
                            versionNumber = "4.5.2 or later";
                        }
                        else if (releaseNumber >= 378675)
                        {
                            versionNumber = "4.5.1 or later";
                        }
                        else if (releaseNumber >= 378389)
                        {
                            versionNumber = "4.5 or later";
                        }
                    }

                    Version = ".NET " + versionNumber;
                }
            }
        }

        /// <summary>
        /// Gets version number of the installed Mono or .NET framework.
        /// </summary>
        public static readonly string Version;
    }
}
