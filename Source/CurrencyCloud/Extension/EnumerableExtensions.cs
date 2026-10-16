using System.Collections.Generic;
using System.Linq;

namespace CurrencyCloud.Extension
{
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Whether two sequences hold equal elements in the same order, where both being null counts as equal and only one being
        /// null does not. Unlike Enumerable.SequenceEqual, a null sequence never throws.
        /// </summary>
        public static bool SequenceEquals<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            return first == second || (first != null && second != null && first.SequenceEqual(second));
        }
    }
}
