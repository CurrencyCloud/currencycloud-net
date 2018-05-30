using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class Iban : Entity
    {
        [JsonConstructor]
        public Iban() { }

        /// <summary>
        /// Id of the IBAN
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// IBAN Code
        /// </summary>
        public string IbanCode { get; set; }

        /// <summary>
        /// Account Id
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [Param]
        public string Currency { get; set; }

        /// <summary>
        /// Name of Account Holder
        /// </summary>
        public string AccountHolderName { get; set; }

        /// <summary>
        /// Name of Bank
        /// </summary>
        public string BankInstitutionName { get; set; }

        /// <summary>
        /// Address of Bank
        /// </summary>
        public string BankInstitutionAddress { get; set; }

        /// <summary>
        /// Country of Bank
        /// </summary>
        public string BankInstitutionCountry { get; set; }

        /// <summary>
        /// BIC or SWIFT code
        /// </summary>
        public string BicSwift { get; set; }

        /// <summary>
        /// IBAN Created At
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// IBAN Updated At
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    AccountId,
                    IbanCode,
                    Currency,
                    AccountHolderName,
                    BankInstitutionName,
                    BankInstitutionAddress,
                    BankInstitutionCountry,
                    BicSwift,
                    CreatedAt,
                    UpdatedAt
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Iban))
            {
                return false;
            }

            var iban = obj as Iban;

            return Id == iban.Id &&
                   IbanCode == iban.IbanCode &&
                   AccountId == iban.AccountId &&
                   Currency == iban.Currency &&
                   AccountHolderName == iban.AccountHolderName &&
                   BankInstitutionName == iban.BankInstitutionName &&
                   BankInstitutionAddress == iban.BankInstitutionAddress &&
                   BankInstitutionCountry == iban.BankInstitutionCountry &&
                   BicSwift == iban.BicSwift &&
                   CreatedAt == iban.CreatedAt &&
                   UpdatedAt == iban.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}