using System;
using System.Collections.Generic;

namespace CurrencyCloud.Entity
{
    public class Payer : Entity
    {
        internal Payer() { }

        public string Id { get; set; }

        public string LegalEntityType { get; set; }

        public string CompanyName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> Address { get; set; }

        public string City { get; set; }

        public string StateOrProvince { get; set; }

        public string IdentificationType { get; set; }

        public string IdentificationValue { get; set; }

        public string Postcode { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
