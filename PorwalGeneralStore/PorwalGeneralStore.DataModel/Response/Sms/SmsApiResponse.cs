using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Response.Sms
{
    public class SmsApiResponse
    {
        public int StatusCode { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public BaseResponse Response { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<SmsApiValidationResponse> ErrorList { get; set; }
    }

    public class SmsApiValidationResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FieldName { get; set; }
    }
}
