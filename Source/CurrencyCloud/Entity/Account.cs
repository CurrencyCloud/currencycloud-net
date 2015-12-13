using System;

namespace CurrencyCloud.Entity
{
    public class Account : Entity
    {
        internal Account() { }

        public string Id { get; set; }

        public string LegalEntityType { get; set; }

        public string AccountName { get; set; }

        public string Brand { get; set; }

        public string YourReference { get; set; }

        public string Status { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string StateOrProvince { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public string SpreadTable { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string IdentificationType { get; set; }

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
