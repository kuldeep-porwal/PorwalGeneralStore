using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Response.Users
{
    public class SignUpFormResponse : BaseResponse
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<SignUpValidationResponse> ErrorList { get; set; }
    }

    public class SignUpValidationResponse : BaseValidationResponse
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FieldName { get; set; }
    }
}
