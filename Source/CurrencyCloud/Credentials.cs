using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud
{
    class Credentials
    {
        public Credentials(string loginId, string apiKey)
        {
            ApiKey = apiKey;
            LoginId = loginId;
        }

        public string ApiKey { get; }
        public string LoginId { get; }
    }
}
