using System.Linq;
using System.Reflection;

namespace CurrencyCloud.Entity
{
    public class Entity
    {
        internal Entity() { }

        public override string ToString()
        {
            var props = from prop in GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        let key = prop.Name
                        let value = prop.GetValue(this)
                        where value != null
                        select string.Format("{0}={1}", key, value);

            return string.Join(", ", props.ToList());
        }
    }
}
