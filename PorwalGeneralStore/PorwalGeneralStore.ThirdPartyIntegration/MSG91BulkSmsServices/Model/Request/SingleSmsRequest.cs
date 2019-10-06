using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request
{
    public class SingleSmsRequest 
    {
        public int country { get; set; }
        public string message { get; set; }
        public string sender { get; set; }
        public string route { get; set; }
        public string mobiles { get; set; }
        public bool? unicode { get; set; }
        public bool? flash { get; set; }
        public DateTime? schtime { get; set; }
        public int afterminutes { get; set; }
        public string response { get; set; }
        public string campaign { get; set; }
    }
}
