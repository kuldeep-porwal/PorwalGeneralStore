using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataModel.Response.Users
{
    public class LoginFormResponse : BaseResponse
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<LoginValidationResponse> ErrorList { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public LoginResponse Response { get; set; }
    }

    public class LoginValidationResponse : BaseValidationResponse
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FieldName { get; set; }
    }

    public class LoginResponse
    {
        public long UserId { get; set; }
        public Token TokenDetail { get; set; }
    }

    public class Token
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
