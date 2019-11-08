using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Request.Sms
{
    public class SingleSmsRequest
    {
        public int Country { get; set; }
        public string Message { get; set; }
        public string Route { get; set; }
        public string Mobiles { get; set; }
    }
}
