using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Request.Sms
{
    public class VerifyOtpRequest
    {
        public string Mobile { get; set; }
        public string Otp { get; set; }
        public int CountryCode { get; set; }
    }
}
