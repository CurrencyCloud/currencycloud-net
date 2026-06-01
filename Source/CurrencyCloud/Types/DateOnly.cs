using System;
using System.Globalization;

namespace CurrencyCloud.Types
{
    internal class DateOnly
    {
        private DateTime _value;

        internal DateOnly(DateTime dateTime)
        {
            _value = dateTime.Date;
        }
        
        public override string ToString()
        {
            return _value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}