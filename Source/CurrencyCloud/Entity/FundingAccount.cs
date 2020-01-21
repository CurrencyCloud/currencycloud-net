using System;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity
{
    public class FundingAccount : Entity
    {
        [JsonConstructor]
        public FundingAccount() { }

        /// <summary>
        /// ID of the Funding Account
        /// </summary>
        public string Id { get; set; }

        ///<summary>
        /// ID of the Account this SSI is attached to
        ///</summary>
        public string AccountId { get; set; }

        ///<summary>
        /// The account number to use when adding funds to the account
        ///</summary>
        public string AccountNumber { get; set; }
        
        ///<summary>
        /// The type of account number provided
        ///</summary>
        public string AccountNumberType { get; set; }
        
        ///<summary>
        /// The name of the account used to make a payment
        ///</summary>
        public string AccountHolderName { get; set; }
        
        ///<summary>
        /// The name of the bank the account is held with
        ///</summary>
        public string BankName { get; set; }
        
        ///<summary>
        /// The address of the bank the account is held with
        ///</summary>
        public string BankAddress { get; set; }
        
        ///<summary>
        /// The 2 digit ISO country code where the bank is based
        ///</summary>
        public string BankCountry { get; set; }
        
        ///<summary>
        /// The currency that should be sent to these account details
        ///</summary>
        public string Currency { get; set; }
        
        ///<summary>
        /// The type of payment that can be made into the account details provided
        ///</summary>
        public string PaymentType { get; set; }
        
        ///<summary>
        /// The particular routing code for this account number
        ///</summary>
        public string RoutingCode { get; set; }
        
        ///<summary>
        /// The type of routing number
        ///</summary>
        public string RoutingCodeType { get; set; }
        
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }


        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    AccountId,
                    AccountNumber,
                    AccountNumberType,
                    AccountHolderName,
                    BankName,
                    BankAddress,
                    BankCountry,
                    Currency,
                    PaymentType,
                    RoutingCode,
                    RoutingCodeType,
                    CreatedAt,
                    UpdatedAt
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FundingAccount))
            {
                return false;
            }

            var account = obj as FundingAccount;

            return Id == account.Id &&
                   AccountId == account.AccountId &&
                   AccountNumber == account.AccountNumber &&
                   AccountNumberType == account.AccountNumberType &&
                   AccountHolderName == account.AccountHolderName &&
                   BankName == account.BankName &&
                   BankAddress == account.BankAddress &&
                   BankCountry == account.BankCountry &&
                   Currency == account.Currency &&
                   PaymentType == account.PaymentType &&
                   RoutingCode == account.RoutingCode &&
                   RoutingCodeType == account.RoutingCodeType &&
                   CreatedAt == account.CreatedAt &&
                   UpdatedAt == account.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
