using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using CurrencyCloud.Extension;

namespace CurrencyCloud
{
    /// <summary>
    /// Represents expandable parameters object.
    /// </summary>
    public class ParamsObject 
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

        internal static ParamsObject CreateFromStaticObject(object obj)
        {
            Type t = obj.GetType();
            ParamsObject ret = new ParamsObject();
            foreach (var p in t.GetProperties())
            {
                var r = Attribute.IsDefined(p, typeof(ParamAttribute));
                if (!r)
                    continue;

                object propValue = p.GetValue(obj);
                if (propValue != null)
                {
                    if (propValue.GetType() == typeof(List<string>))
                    {
                        string newValue = "";
                        foreach (object item in (IList)propValue)
                        {
                            if (newValue.Length > 0)
                                newValue += " \r\n";
                            newValue += item.ToString();
                        }
                        propValue = newValue;
                    }
                    ret.Add(p.Name, propValue);
                }
            }
            return ret;

        }

        public int Count
        {
            get
            {
                return storage.Count;
            }
        }

        public void AddNotNull(string key, object value)
        {
            if (value == null)
                return;
            Add(key, value);
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