using PorwalGeneralStore.HttpWebRequestClientLibrary.Implementation;
using PorwalGeneralStore.HttpWebRequestClientLibrary.Interface;
using PorwalGeneralStore.HttpWebRequestClientLibrary.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestHttpWebRequestClientLibraryUnitTest
{
    public class HttpWebRequestHandlerUnitTest
    {
        private readonly IHttpWebRequestHandler httpWebRequestHandler;
        public HttpWebRequestHandlerUnitTest()
        {
            httpWebRequestHandler = new HttpWebRequestHandler(new HttpWebRequestConfiguration() { KeepAlive = false, UserAgent = "UnitTest" });
        }

        [Fact(DisplayName = "Call Http Get Request with URL,QueryParameter,Header")]
        public void UnitTest1()
        {
            string url = "http://www.tempurl.com";
            Dictionary<string, string> queryParameter = new Dictionary<string, string>();
            Dictionary<string, string> requestHeader = new Dictionary<string, string>();
            var ActualResult = httpWebRequestHandler.Get(url, queryParameter, requestHeader);
            Assert.NotNull(ActualResult);
        }
        [Fact(DisplayName = "Call Http Post Request with URL,PostData,Header")]
        public void UnitTest2()
        {
            string url = "http://www.tempurl.com";
            string postData = "";
            Dictionary<string, string> requestHeader = new Dictionary<string, string>();
            var ActualResult = httpWebRequestHandler.Post(url, postData, requestHeader);
            Assert.NotNull(ActualResult);
        }
        [Fact(DisplayName = "Call Http Get Request with URL,PostData,Header,QueryParameter")]
        public void UnitTest3()
        {
            string url = "http://www.tempurl.com";
            string postData = "";
            Dictionary<string, string> queryParameter = new Dictionary<string, string>();
            Dictionary<string, string> requestHeader = new Dictionary<string, string>();
            var ActualResult = httpWebRequestHandler.Post(url, postData, requestHeader, queryParameter);
            Assert.NotNull(ActualResult);
        }
    }
}
