using Microsoft.Win32;
using System;
using System.Reflection;

namespace CurrencyCloud.Environment
{
    static class Platform
    {
        static Platform()
        {
            Version = System.Environment.Version.ToString();
        }

        /// <summary>
        /// Gets version number of the installed Mono or .NET framework.
        /// </summary>
        public static readonly string Version;
    }
}
