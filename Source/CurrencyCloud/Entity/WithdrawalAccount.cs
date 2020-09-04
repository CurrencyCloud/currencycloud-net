using Newtonsoft.Json;
using System;

namespace CurrencyCloud.Entity
{
    public class WithdrawalAccount : Entity
    {
        [JsonConstructor]
        public WithdrawalAccount() { }
            
        /// <summary>
        /// Id of the Withdrawal Account
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Account name
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Account holder name
        /// </summary>
        public string AccountHolderName { get; set; }

        /// <summary>
        /// Account holder date of birth
        /// </summary>
        public DateTime? AccountHolderDob { get; set; }

        /// <summary>
        /// Routing Code
        /// </summary>
        public string RoutingCode { get; set; }

        /// <summary>
        /// Account number
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Account Id
        /// </summary>
        public string AccountId { get; set; }


        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Id,
                    AccountName,
                    AccountHolderName,
                    AccountHolderDob,
                    RoutingCode,
                    AccountNumber,
                    Currency,
                    AccountId
                }
            };
            return JsonConvert.SerializeObject(obj);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is WithdrawalAccount))
            {
                return false;
            }

            var withdrawalAccount = obj as WithdrawalAccount;

            return Id == withdrawalAccount.Id &&
                   AccountName == withdrawalAccount.AccountName &&
                   AccountHolderName == withdrawalAccount.AccountHolderName &&
                   AccountHolderDob == withdrawalAccount.AccountHolderDob &&
                   RoutingCode == withdrawalAccount.RoutingCode &&
                   AccountNumber == withdrawalAccount.AccountNumber &&
                   Currency == withdrawalAccount.Currency &&
                   AccountId == withdrawalAccount.AccountId;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}