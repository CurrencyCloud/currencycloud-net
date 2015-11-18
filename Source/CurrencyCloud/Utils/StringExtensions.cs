using System.Text.RegularExpressions;

namespace CurrencyCloud.Utils
{
    public static class StringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSnakeCase(this string value)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            string snake = Regex.Replace(value, "([A-Z0-9])", "_$1").ToLower();
            if(snake.StartsWith("_"))
            {
                snake = snake.Substring(1);
            }

            return snake;
        }
    }
}
