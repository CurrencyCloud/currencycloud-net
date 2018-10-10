using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.List
{
    public class BeneficiaryDetailsList
    {
        internal BeneficiaryDetailsList() { }

        public struct Detail
        {
            public string PaymentType { get; set; }
            public string AcctNumber { get; set; }
            public string BicSwift { get; set; }
            public string BeneficiaryEntityType { get; set; }
            public string BeneficiaryStateOrProvince { get; set; }
            public string BeneficiaryPostcode { get; set; }
            public string BeneficiaryAddress { get; set; }
            public string BeneficiaryCity { get; set; }
            public string BeneficiaryCountry { get; set; }
            public string BeneficiaryFirstName { get; set; }
            public string BeneficiaryLastName { get; set; }
            public string BeneficiaryCompanyName { get; set; }
            public string Aba { get; set; }
            public string SortCode { get; set; }
            public string Iban { get; set; }
            public string BsbCode { get; set; }
            public string InstitutionNo { get; set; }
            public string BankCode { get; set; }
            public string BranchCode { get; set; }
            public string Clabe { get; set; }
            public string Cnaps { get; set; }
            public string Ifsc { get; set; }
        }

        public List<Detail> Details { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Details
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
