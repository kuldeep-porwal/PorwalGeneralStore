using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace PorwalGeneralStore.Global.ExtensionMethods
{
    public static class HttpWebResponseExtensionsMethods
    {
        public static string ResponseAsString(this HttpWebResponse httpWebResponse)
        {
            string response = string.Empty;
            using (var stream = httpWebResponse.GetResponseStream())
            {
                var reader = new StreamReader(stream);
                response = reader.ReadToEnd();
            }
            return response;
        }
        public static string ResponseAsString(this HttpWebResponse httpWebResponse, Encoding encodingType)
        {
            string response = string.Empty;
            using (var stream = httpWebResponse.GetResponseStream())
            {
                var reader = new StreamReader(stream, encodingType);
                response = reader.ReadToEnd();
            }
            return response;
        }
    }
}
