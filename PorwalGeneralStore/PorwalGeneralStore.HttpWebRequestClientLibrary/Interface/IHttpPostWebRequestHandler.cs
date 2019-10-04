using PorwalGeneralStore.HttpWebRequestClientLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.HttpWebRequestClientLibrary.Interface
{
    public interface IHttpPostWebRequestHandler
    {
        BaseHttpWebResponse Post(string url, string postData, Dictionary<string, string> requestHeader);
        BaseHttpWebResponse Post(string url, string postData, Dictionary<string, string> requestHeader, Dictionary<string, string> queryParameter);
    }
}
