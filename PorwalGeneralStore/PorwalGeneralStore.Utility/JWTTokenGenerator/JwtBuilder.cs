using Microsoft.IdentityModel.Tokens;
using PorwalGeneralStore.Utility.JWTTokenGenerator.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PorwalGeneralStore.Utility.JWTTokenGenerator
{
    public class JwtBuilder : IJwtBuilder
    {
        private readonly JwtConfiguration _config;
        private readonly SigningCredentials _signingCredentials;

        private JwtBuilder()
        {
        }

        public JwtBuilder(JwtConfiguration config)
        {
            _config = config;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config.Secret));
            _signingCredentials = new SigningCredentials(secretKey, this._config.Algorithm);
        }

        public JwtTokenResponse GetJWTToken(Claim[] claims, string issuer, string audience, DateTime? expires)
        {
            throw new NotImplementedException();
        }
    }
}
