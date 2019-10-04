using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PorwalGeneralStore.Global.ExtensionMethods
{
    public static class DictionaryExtensionsMethods
    {
        public static string GetQueryString(this Dictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return null;
            }
            return parameters.Join("&", "=");
        }

        public static string GetQueryString(this Dictionary<string, string> parameters, bool urlEncode)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return null;
            }
            return parameters.Join("&", "=", urlEncode);
        }

        public static string Join(this
            Dictionary<string, string> source,
            string keyValuePairSeparator,
            string keyValueSeparator
            )
        {
            return Join(
                source,
                keyValuePairSeparator,
                keyValueSeparator,
                false
                );
        }

        public static string Join(this
            Dictionary<string, string> source,
            string keyValuePairSeparator,
            string keyValueSeparator,
            bool urlEncode
            )
        {
            var builder = new StringBuilder();

            int count = 0;

            foreach (var parameter in source)
            {
                count++;
                builder.AppendFormat("{0}{1}{2}", parameter.Key, keyValueSeparator, urlEncode ? Uri.EscapeDataString(parameter.Value) : parameter.Value);
                if (count != source.Keys.Count)
                    builder.Append(keyValuePairSeparator);
            }

            return builder.ToString();
        }
    }
}
