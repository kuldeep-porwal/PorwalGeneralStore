using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Request.Sms
{
    public class ResendSmsOtpRequest 
    {
        public string retrytype { get; set; }
        public long mobile { get; set; }
    }
}
