using PorwalGeneralStore.HttpWebRequestClientLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.HttpWebRequestClientLibrary.Interface
{
    public interface IHttpGetWebRequestHandler
    {
        BaseHttpWebResponse Get(string url, Dictionary<string, string> queryParameter, Dictionary<string, string> requestHeader);
    }
}
