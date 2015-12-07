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
    }
}
