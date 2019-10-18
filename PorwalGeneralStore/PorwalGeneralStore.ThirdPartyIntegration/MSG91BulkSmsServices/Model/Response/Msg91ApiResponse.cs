using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Response
{
    public class Msg91ApiResponse
    {
        public int StatusCode { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public BaseResponse MessageResponse { get; set; }
    }
}
