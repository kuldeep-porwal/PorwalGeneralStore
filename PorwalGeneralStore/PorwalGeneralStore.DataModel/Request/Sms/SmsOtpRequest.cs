using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Request.Sms
{
    public class SmsOtpRequest
    {
        public int CountryCode { get; set; }
        public int Otp { get; set; }
        public string MessageTemplate { get; set; }
        public string Mobile { get; set; }
    }
}
