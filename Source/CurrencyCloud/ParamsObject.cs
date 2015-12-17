using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Collections;
using CurrencyCloud.Extension;

namespace CurrencyCloud
{
    /// <summary>
    /// Represents expandable parameters object.
    /// </summary>
    public class ParamsObject : DynamicObject, IEnumerable
    {
        private Dictionary<string, object> storage;

        private void Expand(ParamsObject paramsObj)
        {
            foreach (KeyValuePair<string, object> param in paramsObj)
            {
                storage.Add(param.Key, param.Value);
            }
        }

        public static ParamsObject operator +(ParamsObject paramsObj1, ParamsObject paramsObj2)
        {
            var res = new ParamsObject();

            if (paramsObj1 != null)
            {
                res.Expand(paramsObj1);
            }

            if (paramsObj2 != null)
            {
                res.Expand(paramsObj2);
            }

            return res;
        }

        public ParamsObject()
        {
            storage = new Dictionary<string, object>();
        }

        public ParamsObject(dynamic dynamicObj) : this()
        {
            var props = dynamicObj.GetType().GetProperties();
            foreach (var prop in props)
            {
                string key = prop.Name;
                object value = prop.GetValue(dynamicObj);

                storage.Add(key.ToSnakeCase(), value);
            }
        }

        public int Count
        {
            get
            {
                return storage.Count;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var key = binder.Name.ToSnakeCase();

            return storage.TryGetValue(key, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var key = binder.Name.ToSnakeCase();

            storage[key] = value;

            return true;
        }

        public void Add(string key, object value)
        {
            storage.Add(key.ToSnakeCase(), value);
        }

        public bool Remove(string key)
        {
            return storage.Remove(key.ToSnakeCase());
        }

        public IEnumerator GetEnumerator()
        {
            return storage.GetEnumerator();
        }

        internal string ToQueryString()
        {
            return string.Join("&", storage.Select(param =>
            {
                string key = param.Key;

                if (param.Value is Array)
                {
                    var values = from object item in param.Value as Array
                                 select key + "[]=" + item.ToString();

                    return string.Join("&", values.ToList());
                }

                string value;
                if (param.Value is DateTime)
                {
                    value = ((DateTime)param.Value).ToString("yyyy-MM-dd");
                }
                else if (param.Value is bool)
                {
                    value = param.Value.ToString().ToLower();
                }
                else
                {
                    value = param.Value.ToString();
                }
                return key + "=" + value;
            }));
        }
    }
}