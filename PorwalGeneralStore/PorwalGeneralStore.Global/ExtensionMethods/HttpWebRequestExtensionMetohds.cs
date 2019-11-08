using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace PorwalGeneralStore.Global.ExtensionMethods
{
    public static class HttpWebRequestExtensionMetohds
    {
        public static void AddRequestBody(this HttpWebRequest webRequest, string postString)
        {
            if (!string.IsNullOrWhiteSpace(postString))
            {
                byte[] postBytes = Encoding.ASCII.GetBytes(postString);

                Stream postStream = webRequest.GetRequestStream();
                postStream.Write(postBytes, 0, postBytes.Length);
                postStream.Close();
            }
        }
    }
}
