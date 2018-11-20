using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.List
{
    public class PaymentAuthorisationsList
    {
        internal PaymentAuthorisationsList() { }

        public struct Authorisation
        {
            public string PaymentId { get; set; }
            public string PaymentStatus { get; set; }
            public bool Updated { get; set; }
            public string Error { get; set; }
            public int AuthStepsTaken { get; set; }
            public int AuthStepsRequired { get; set; }
            public string ShortReference { get; set; }
        }

        public List<Authorisation> Authorisations { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Authorisations
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}