using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.Utility.JWTTokenGenerator.Model
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }
        public string Algorithm { get; set; } = SecurityAlgorithms.HmacSha256;
        public string Issuer { get; set; }
    }
}
