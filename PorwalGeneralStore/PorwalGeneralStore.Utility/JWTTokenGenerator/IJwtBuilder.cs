using PorwalGeneralStore.Utility.JWTTokenGenerator.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PorwalGeneralStore.Utility.JWTTokenGenerator
{
    public interface IJwtBuilder
    {
        JwtTokenResponse GetJWTToken(Dictionary<string, string> claims, string audience = null, DateTime? expires = null);
    }
}
