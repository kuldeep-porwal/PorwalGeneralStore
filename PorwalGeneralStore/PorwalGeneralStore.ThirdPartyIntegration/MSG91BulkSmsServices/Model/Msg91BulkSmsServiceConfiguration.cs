using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model
{
    public class Msg91BulkSmsServiceConfiguration
    {
        public string BaseApiUrl { get; set; }
        public string SendOtpApiUrl { get; set; }
        public string AuthKey { get; set; }
        public string SenderId { get; set; } = "MSGPGS";
    }
}
