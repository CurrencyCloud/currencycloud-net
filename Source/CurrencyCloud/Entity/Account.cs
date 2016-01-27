using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyCloud.Entity
{
    public class Account : Entity
    {
        public Account(string accountName, string legalEntityType)
        {
            this.AccountName = accountName;
            this.LegalEntityType = legalEntityType;
        }

        [Newtonsoft.Json.JsonConstructor]
        internal Account() { }

        public string Id { get; set; }

        [Param]
        public string LegalEntityType { get; set; }

        [Param]
        public string AccountName { get; set; }

        [Param]
        public string Brand { get; set; }

        [Param]
        public string YourReference { get; set; }

        [Param]
        public string Status { get; set; }

        [Param]
        public string Street { get; set; }

        [Param]
        public string City { get; set; }

        [Param]
        public string StateOrProvince { get; set; }

        [Param]
        public string Country { get; set; }

        [Param]
        public string PostalCode { get; set; }

        [Param]
        public string SpreadTable { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [Param]
        public string IdentificationType { get; set; }

        [Param]
        public string IdentificationValue { get; set; }

        public string ShortReference { get; set; }

        public override bool Equals(object obj)
        {
            if(!(obj is Account))
            {
                return false;
            }

            var account = obj as Account;

            return Id == account.Id &&
                   LegalEntityType == account.LegalEntityType &&
                   AccountName == account.AccountName &&
                   Brand == account.Brand &&
                   YourReference == account.YourReference &&
                   Status == account.Status &&
                   Street == account.Street &&
                   City == account.City &&
                   StateOrProvince == account.StateOrProvince &&
                   Country == account.Country &&
                   PostalCode == account.PostalCode &&
                   SpreadTable == account.SpreadTable &&
                   CreatedAt == account.CreatedAt &&
                   UpdatedAt == account.UpdatedAt &&
                   IdentificationType == account.IdentificationType &&
                   IdentificationValue == account.IdentificationValue &&
                   ShortReference == account.ShortReference;

        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
