using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.Utility.JWTTokenGenerator.Model
{
    public class JwtTokenResponse
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiredAt { get; set; }
    }
}
