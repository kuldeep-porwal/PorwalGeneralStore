using PorwalGeneralStore.Global.ExtensionMethods;
using PorwalGeneralStore.HttpWebRequestClientLibrary.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace PorwalGeneralStore.HttpWebRequestClientLibrary
{
    public abstract class HttpWebRequestBuilder
    {
        private readonly HttpWebRequestConfiguration _webRequestConfiguration;

        protected HttpWebRequestBuilder(HttpWebRequestConfiguration webRequestConfiguration)
        {
            _webRequestConfiguration = webRequestConfiguration;
        }
        public HttpWebRequest BuildRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.KeepAlive = _webRequestConfiguration.KeepAlive;
            request.UserAgent = _webRequestConfiguration.UserAgent;

            return request;
        }

        public HttpWebRequest BuildRequest(
            string uri,
            Dictionary<string, string> parameters
            )
        {
            string query = parameters.GetQueryString();
            string url = !string.IsNullOrWhiteSpace(query) ? String.Format("{0}?{1}", uri, query) : uri;

            return BuildRequest(url);
        }

        public HttpWebRequest BuildRequest(
            string uri,
            Dictionary<string, string> parameters,
            bool urlEncode
            )
        {
            string query = parameters.GetQueryString(urlEncode);
            string url = !string.IsNullOrWhiteSpace(query) ? String.Format("{0}?{1}", uri, query) : uri;

            return BuildRequest(url);
        }

        public BaseHttpWebResponse GetResponse(HttpWebRequest httpWebRequest)
        {
            BaseHttpWebResponse webResponse = new BaseHttpWebResponse();
            try
            {
                HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                webResponse.StatusCode = (int)httpWebResponse.StatusCode;
                webResponse.WebResponse = httpWebResponse;
                webResponse.Response = httpWebResponse.ResponseAsString();
            }
            catch (WebException webEx)
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)webEx.Response;
                if (httpWebResponse != null)
                {
                    webResponse.webException = webEx;
                    webResponse.WebResponse = httpWebResponse;
                    webResponse.StatusCode = (int)httpWebResponse.StatusCode;
                    webResponse.ErrorResponse = httpWebResponse.ResponseAsString();
                }
            }
            return webResponse;
        }
    }
}
