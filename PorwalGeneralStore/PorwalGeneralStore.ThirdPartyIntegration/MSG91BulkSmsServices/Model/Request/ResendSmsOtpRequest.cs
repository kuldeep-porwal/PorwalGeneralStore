using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request
{
    public class ResendSmsOtpRequest 
    {
        public string retrytype { get; set; }
        public long mobile { get; set; }
    }
}
