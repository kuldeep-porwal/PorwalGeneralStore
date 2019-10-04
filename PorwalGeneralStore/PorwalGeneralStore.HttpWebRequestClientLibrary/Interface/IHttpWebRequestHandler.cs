using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.HttpWebRequestClientLibrary.Interface
{
    public interface IHttpWebRequestHandler : IHttpGetWebRequestHandler, IHttpPostWebRequestHandler, IHttpPutWebRequestHandler, IHttpPatchWebRequestHandler, IHttpDeleteWebRequestHandler
    {

    }
}
