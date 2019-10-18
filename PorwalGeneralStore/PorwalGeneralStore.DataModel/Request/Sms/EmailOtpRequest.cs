using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Request.Sms
{
    public class EmailOtpRequest 
    {
        public string otp { get; set; }
        public string email { get; set; }
    }
}
