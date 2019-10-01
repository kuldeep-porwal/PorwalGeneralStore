using PorwalGeneralStore.Utility.JWTTokenGenerator.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PorwalGeneralStore.Utility.JWTTokenGenerator
{
    public interface IJwtBuilder
    {
        JwtTokenResponse GetJWTToken(Claim[] claims, string issuer = null, string audience = null, DateTime? expires = null);
    }
}
