using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCloud.Entity
{
    public class BeneficiaryFindParameters : FindParameters
    {
        [Param]
        public string BankAccountHolderName { get; set; }

        [Param]
        public string Name { get; set; }

        [Param]
        public string PaymentTypes { get; set; }

        [Param]
        public string BeneficiaryCountry { get; set; }

        [Param]
        public string BeneficiaryEntityType { get; set; }

        [Param]
        public string BeneficiaryCompanyName { get; set; }

        [Param]
        public string BeneficiaryFirstName { get; set; }

        [Param]
        public string BeneficiaryLastName { get; set; }

        [Param]
        public string BeneficiaryCity { get; set; }

        [Param]
        public string BeneficiaryPostcode { get; set; }

        [Param]
        public string BeneficiaryStateOrProvince { get; set; }

        [Param]
        public DateTime? BeneficiaryDateOfBirth { get; set; }

        [Param]
        public string BankName { get; set; }

        [Param]
        public string BankAccountType { get; set; }

        [Param]
        public string Currency { get; set; }

        [Param]
        public string AccountNumber { get; set; }

        [Param]
        public string RoutingCodeType { get; set; }

        [Param]
        public string RoutingCodeValue { get; set; }

        [Param]
        public string BicSwift { get; set; }

        [Param]
        public string Iban { get; set; }

        [Param]
        public bool? DefaultBeneficiary { get; set; }

    }
}
