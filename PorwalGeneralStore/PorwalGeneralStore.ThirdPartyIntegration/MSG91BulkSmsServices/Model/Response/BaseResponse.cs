using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Response
{
    public class BaseResponse
    {
        public string Message { get; set; }
        public string Type { get; set; }
        public string RequestId { get; set; }
    }
}
