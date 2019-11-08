using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request
{
    public class Msg91SmsOtpRequest
    {
        public string email { get; set; }
        public int otp { get; set; }
        public int otp_length { get; set; }
        public int otp_expiry { get; set; }
        public string sender { get; set; }
        public string template_id { get; set; }
        public string mobile { get; set; }
    }
}
