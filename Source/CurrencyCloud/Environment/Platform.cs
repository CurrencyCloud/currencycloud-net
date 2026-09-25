using System.Runtime.InteropServices;

namespace CurrencyCloud.Environment
{
    /// <summary>
    /// Provides runtime framework information.
    /// </summary>
    internal static class Platform
    {
        /// <summary>
        /// Gets the framework description.
        /// </summary>
        public static readonly string Version = RuntimeInformation.FrameworkDescription;
    }
}