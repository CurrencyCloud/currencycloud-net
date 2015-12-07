using System.Collections.Generic;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedContacts
    {
        internal PaginatedContacts() { }

        public List<Contact> Contacts { get; set; }

        public Pagination Pagination { get; set; }
    }
}
