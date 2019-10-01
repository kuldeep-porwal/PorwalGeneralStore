using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.Utility.JWTTokenGenerator.Model
{
    public class JwtToken
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiredAt { get; set; }
    }

    public class JwtTokenResponse : BaseJwtTokenResponse
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<JwtValidation> ErrorList { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public JwtToken TokenDetail { get; set; }
    }

    public class JwtValidation
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }

    public class BaseJwtTokenResponse
    {
        public int StatusCode { get; set; }
    }
}
