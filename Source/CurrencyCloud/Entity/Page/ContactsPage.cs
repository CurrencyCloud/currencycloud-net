using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CurrencyCloud.Entity.Page
{
    [DataContract]
    public class ContactsPage
    {
        [DataMember(Name = "contacts")]
        public List<Contact> Contacts { get; internal set; }

        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; internal set; }

        internal ContactsPage() { }
    }
}
