﻿using CurrencyCloud.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using CurrencyCloud.Attributes;
using CurrencyCloud.Types;

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
            if (obj == null)
                return null;

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
                    if (propValue is DateTime)
                    {
                        var dtOnly = Attribute.IsDefined(p, typeof(DateOnlyAttribute));

                        if (dtOnly)
                            propValue = new DateOnly((DateTime)propValue);
                    }

                    ret.Add(p.Name, propValue);
                }
            }
            return ret;

        }

        public FormUrlEncodedContent buildFormUrlBodyFromParams()
        {
            List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>();
            foreach (KeyValuePair<string, object> param in storage)
            {
                if (param.Value is Array)
                {
                    foreach (var value in (string[])param.Value)
                    {
                        KeyValuePair<string, object> entry = new KeyValuePair<string, object>(param.Key, value);
                        paramList.Add(new KeyValuePair<string, string>(param.Key + "[]", formatValue(entry)));
                    }
                }
                else
                {
                    paramList.Add(new KeyValuePair<string, string>(param.Key, formatValue(param)));
                }
            }
            return new FormUrlEncodedContent(paramList);
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
                    var dt = (DateTime)param.Value;
                    var utc = (dt.Kind == DateTimeKind.Utc) ? dt : dt.ToUniversalTime();
                    value = utc.ToString("O", CultureInfo.InvariantCulture);
                }
                else if (param.Value is DateOnly)
                {
                    value = param.Value.ToString();
                }
                else if (param.Value is bool)
                {
                    value = param.Value.ToString().ToLower();
                }
                else if (param.Value is decimal)
                {
                    value = ((decimal)param.Value).ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                else if (param.Value is Enum)
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

        internal string formatValue(KeyValuePair<string, object> param)
        {
            if (param.Value is Array)
            {
                var values = from object item in param.Value as Array
                             select param.Key + "[]=" + item.ToString();

                return string.Join("&", values.ToList());
            }
            String value;
            if (param.Value is DateTime)
            {
                var dt = (DateTime)param.Value;
                var utc = (dt.Kind == DateTimeKind.Utc) ? dt : dt.ToUniversalTime();
                value = utc.ToString("O", CultureInfo.InvariantCulture);
            }
            else if (param.Value is DateOnly)
            {
                value = param.Value.ToString();
            }
            else if (param.Value is bool)
            {
                value = param.Value.ToString().ToLower();
            }
            else if (param.Value is decimal)
            {
                value = ((decimal)param.Value).ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            else if (param.Value is Enum)
            {
                value = param.Value.ToString().ToLower();
            }
            else
            {
                value = param.Value.ToString();
            }
            return value;
        }
    }
}