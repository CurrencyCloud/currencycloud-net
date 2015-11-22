using System;
using System.Text.RegularExpressions;

namespace CurrencyCloud.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a copy of this string converted to PascalCase.
        /// </summary>
        public static string ToPascalCase(this string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            string pascal = Regex.Replace(source, "[_]([a-z0-9])", match => match.Value.Substring(1).ToUpper());

            pascal = char.ToUpper(pascal[0]) + pascal.Substring(1);

            return pascal;
        }

        /// <summary>
        /// Returns a copy of this string converted to snake_case.
        /// </summary>
        public static string ToSnakeCase(this string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            string snake = Regex.Replace(source, "([A-Z0-9])", "_$1").ToLower();
            if(snake.StartsWith("_"))
            {
                snake = snake.Substring(1);
            }

            return snake;
        }
    }
}
