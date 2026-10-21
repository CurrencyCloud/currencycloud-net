using System;

namespace CurrencyCloud
{
    class ParamAttribute : Attribute
    {
        /// <summary>
        /// Send a string collection as a single line-joined value instead of an array (field[]=value).
        /// </summary>
        public bool JoinLines { get; set; }
    }
}
