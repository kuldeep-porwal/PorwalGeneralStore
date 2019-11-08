using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Request.Sms
{
    public class SmsOtpRequest
    {
        public string CountryCode { get; set; }
        public int Otp { get; set; }
        public string Message { get; set; }
        public string Mobile { get; set; }
    }
}
