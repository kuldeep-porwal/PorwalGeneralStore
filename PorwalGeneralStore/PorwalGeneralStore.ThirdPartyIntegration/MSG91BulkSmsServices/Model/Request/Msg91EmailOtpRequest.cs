using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request
{
    public class Msg91EmailOtpRequest 
    {
        public string otp { get; set; }
        public string email { get; set; }
    }
}
