using System.Collections.Generic;

namespace CurrencyCloud.Entity
{
    public interface ResponseAware
    {
        Dictionary<string, string> Headers { get; set; }
    }
}