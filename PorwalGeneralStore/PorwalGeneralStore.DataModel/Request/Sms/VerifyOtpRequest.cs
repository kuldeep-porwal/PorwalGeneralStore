using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Request.Sms
{
    public class VerifyOtpRequest 
    {
        public string mobile { get; set; }
        public string otp { get; set; }
    }
}
