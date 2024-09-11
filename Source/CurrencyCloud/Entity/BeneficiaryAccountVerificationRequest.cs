using Newtonsoft.Json;
using System;

namespace CurrencyCloud.Entity
{
    public class BeneficiaryAccountVerificationRequest : Entity
    {
        [JsonConstructor]
        public BeneficiaryAccountVerificationRequest() { }

        [Param]
        public string AccountNumber { get; set; }

        [Param]
        public string BeneficiaryEntityType { get; set; }

        [Param]
        public string BeneficiaryCompanyName { get; set; }

        [Param]
        public string BeneficiaryFirstName { get; set; }

        [Param]
        public string BeneficiaryLastName { get; set; }

        [Param]
        public string SecondaryReferenceData { get; set; }

        [Param]
        public string BankCountry { get; set; }

        [Param]
        public string RoutingCodeType1 { get; set; }

        [Param]
        public string RoutingCodeValue1 { get; set; }

        public static BeneficiaryAccountVerificationRequest Create()
        {
            return new BeneficiaryAccountVerificationRequest();
        }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    AccountNumber,
                    BeneficiaryEntityType,
                    BeneficiaryCompanyName,
                    BeneficiaryFirstName,
                    BeneficiaryLastName,
                    SecondaryReferenceData,
                    BankCountry,
                    RoutingCodeType1,
                    RoutingCodeValue1
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override int GetHashCode()
        {
            return AccountNumber.GetHashCode();
        }
    }
}