using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model
{
    public class Msg91BulkSmsServiceConfiguration
    {
        public string AuthKey { get; set; }
        public string SenderId { get; set; } = "MSGPGS";
    }
}
