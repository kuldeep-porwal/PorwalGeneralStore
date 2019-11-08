using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request
{
    public class Msg91VerifyOtpRequest 
    {
        public string mobile { get; set; }
        public string otp { get; set; }
    }
}
