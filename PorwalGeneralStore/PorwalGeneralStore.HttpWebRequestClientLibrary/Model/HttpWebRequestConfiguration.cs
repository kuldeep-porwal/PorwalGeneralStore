using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.HttpWebRequestClientLibrary.Model
{
    public class HttpWebRequestConfiguration
    {
        public bool KeepAlive { get; set; } = false;
        public string UserAgent { get; set; }
    }
}
