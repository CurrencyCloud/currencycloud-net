using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class VirtualAccount : Entity
    {
        [JsonConstructor]
        public VirtualAccount() { }

        /// <summary>
        /// Id of the Virtual Account
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Virtual Account Number
        /// </summary>
        public string VirtualAccountNumber { get; set; }

        /// <summary>
        /// Account Id
        /// </summary>
        public string AccountId { get; set; }

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
        /// Routing Code
        /// </summary>
        public string RoutingCode { get; set; }

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
                    VirtualAccountNumber,
                    AccountHolderName,
                    BankInstitutionName,
                    BankInstitutionAddress,
                    BankInstitutionCountry,
                    RoutingCode,
                    CreatedAt,
                    UpdatedAt
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is VirtualAccount))
            {
                return false;
            }

            var virtualAccountNumber = obj as VirtualAccount;

            return Id == virtualAccountNumber.Id &&
                   VirtualAccountNumber == virtualAccountNumber.VirtualAccountNumber &&
                   AccountId == virtualAccountNumber.AccountId &&
                   AccountHolderName == virtualAccountNumber.AccountHolderName &&
                   BankInstitutionName == virtualAccountNumber.BankInstitutionName &&
                   BankInstitutionAddress == virtualAccountNumber.BankInstitutionAddress &&
                   BankInstitutionCountry == virtualAccountNumber.BankInstitutionCountry &&
                   RoutingCode == virtualAccountNumber.RoutingCode &&
                   CreatedAt == virtualAccountNumber.CreatedAt &&
                   UpdatedAt == virtualAccountNumber.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}