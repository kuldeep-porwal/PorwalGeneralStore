using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Response.Orders
{
    public class UpdateOrderAddressFormResponse : BaseResponse
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<UpdateOrderAddressFormValidationResponse> ErrorList { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; set; }
    }

    public class UpdateOrderAddressFormValidationResponse : BaseValidationResponse
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FieldName { get; set; }
    }
}
