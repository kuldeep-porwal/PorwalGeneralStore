using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request
{
    public class SmsOtpRequest 
    {
        public string email { get; set; }
        public int template { get; set; }
        public int otp { get; set; }
        public int otp_length { get; set; }
        public int otp_expiry { get; set; }
        public string sender { get; set; }
        public string message { get; set; }
        public long mobile { get; set; }
    }
}
