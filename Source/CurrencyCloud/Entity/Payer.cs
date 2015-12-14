using System;
using System.Linq;

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

        public string Address { get; set; }

        public string City { get; set; }

        public string StateOrProvince { get; set; }

        public string Country { get; set; }

        public string IdentificationType { get; set; }

        public string IdentificationValue { get; set; }

        public string Postcode { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Payer))
            {
                return false;
            }

            var payer = obj as Payer;

            return Id == payer.Id &&
                   LegalEntityType == payer.LegalEntityType &&
                   CompanyName == payer.CompanyName &&
                   FirstName == payer.FirstName &&
                   LastName == payer.LastName &&
                   Address.SequenceEqual(payer.Address) &&
                   City == payer.City &&
                   StateOrProvince == payer.StateOrProvince &&
                   IdentificationType == payer.IdentificationType &&
                   IdentificationValue == payer.IdentificationValue &&
                   Postcode == payer.Postcode &&
                   DateOfBirth == payer.DateOfBirth &&
                   CreatedAt == payer.CreatedAt &&
                   UpdatedAt == payer.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
