using PorwalGeneralStore.Global.ExtensionMethods;
using PorwalGeneralStore.HttpWebRequestClientLibrary.Interface;
using PorwalGeneralStore.HttpWebRequestClientLibrary.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PorwalGeneralStore.HttpWebRequestClientLibrary.Implementation
{
    public class HttpWebRequestHandler : HttpWebRequestBuilder, IHttpWebRequestHandler
    {
        public HttpWebRequestHandler(HttpWebRequestConfiguration webRequestConfiguration) : base(webRequestConfiguration)
        {
        }
        public BaseHttpWebResponse Get(string url, Dictionary<string, string> queryParameter, Dictionary<string, string> requestHeader)
        {
            var request = BuildRequest(url, queryParameter);
            request.Method = WebRequestMethods.Http.Get;
            return GetResponse(request);
        }

        public BaseHttpWebResponse Post(string url, string postData, Dictionary<string, string> requestHeader)
        {
            var request = BuildRequest(url);
            request.Method = WebRequestMethods.Http.Post;
            request.AddRequestBody(postData);
            return GetResponse(request);
        }

        public BaseHttpWebResponse Post(string url, string postData, Dictionary<string, string> requestHeader, Dictionary<string, string> queryParameter)
        {
            var request = BuildRequest(url, queryParameter);
            request.Method = WebRequestMethods.Http.Post;
            request.AddRequestBody(postData);
            return GetResponse(request);
        }
    }
}
