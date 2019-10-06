using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.Utility
{
    public static class StringUtility
    {
        public static string ConvertObjectToJson(object obj, bool isThrowException = false)
        {
            string json = string.Empty;
            try
            {
                if (obj == null)
                {
                    json = null;
                    return json;
                }
                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                };
                json = JsonConvert.SerializeObject(obj, jsonSerializerSettings);
            }
            catch
            {
                if (isThrowException)
                {
                    throw;
                }
            }
            return json;
        }

        public static T ConvertJsonToObject<T>(string json, bool isThrowException = false)
        {
            T convertedObject = default(T);
            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    return default(T);
                }
                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                };
                convertedObject = JsonConvert.DeserializeObject<T>(json, jsonSerializerSettings);
            }
            catch
            {
                if (isThrowException)
                {
                    throw;
                }
            }
            return convertedObject;
        }
    }
}
