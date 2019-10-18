using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Response.Orders
{
    public class CancelOrderFormResponse : BaseResponse
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<CancelOrderFormValidationResponse> ErrorList { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; set; }
    }

    public class CancelOrderFormValidationResponse : BaseValidationResponse
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FieldName { get; set; }
    }
}
