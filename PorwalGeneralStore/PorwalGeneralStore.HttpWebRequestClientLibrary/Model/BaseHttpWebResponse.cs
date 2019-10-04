using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PorwalGeneralStore.HttpWebRequestClientLibrary.Model
{
    public class BaseHttpWebResponse
    {
        public int StatusCode { get; set; }
        public string Response { get; set; }
        public string ErrorResponse { get; set; }
        public HttpWebResponse WebResponse { get; set; }
        public WebException webException { get; set; }
    }
}
