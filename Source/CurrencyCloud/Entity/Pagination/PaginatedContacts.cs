using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyCloud.Entity.Pagination
{
    public class PaginatedContacts
    {
        internal PaginatedContacts() { }

        public List<Contact> Contacts { get; set; }

        public Pagination Pagination { get; set; }

        public string ToJSON()
        {
            var obj = new[]
            {
                new
                {
                    Contacts,
                    Pagination
                }
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
