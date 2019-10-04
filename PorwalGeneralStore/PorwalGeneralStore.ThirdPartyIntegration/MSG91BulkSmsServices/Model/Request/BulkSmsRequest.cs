using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request
{
    public class SmsRequestFormat
    {
        public string message { get; set; }
        public List<string> to { get; set; }
    }

    public class BulkSmsRequest
    {
        public string sender { get; set; }
        public string route { get; set; }
        public string country { get; set; }
        public List<SmsRequestFormat> sms { get; set; }
    }
}
