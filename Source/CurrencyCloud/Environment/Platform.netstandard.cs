using System.Reflection;
using System.Runtime.Versioning;

namespace CurrencyCloud.Environment
{
    internal static class Platform
    {
        static Platform()
        {
            Version = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName ?? ".NET";
        }

        /// <summary>
        /// Gets version number of the installed Mono or .NET framework.
        /// </summary>
        public static readonly string Version;
    }
}
