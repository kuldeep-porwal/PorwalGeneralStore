using Microsoft.IdentityModel.Tokens;
using PorwalGeneralStore.Utility.JWTTokenGenerator.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));
            _signingCredentials = new SigningCredentials(secretKey, _config.Algorithm);
        }

        public JwtTokenResponse GetJWTToken(Dictionary<string, string> claims, string audience = null, DateTime? expires = null)
        {
            DateTime issuedAt = DateTime.UtcNow;
            JwtTokenResponse jwtTokenResponse = new JwtTokenResponse()
            {
                StatusCode = 200
            };

            if (claims == null ||
                claims.Count == 0)
            {
                jwtTokenResponse.StatusCode = 400;
                jwtTokenResponse.ErrorList = new List<JwtValidation>()
                {
                    new JwtValidation()
                    {
                        FieldName=nameof(claims),
                        Message="Claims can't be blank"
                    }
                };
                return jwtTokenResponse;
            }

            if (string.IsNullOrWhiteSpace(audience))
            {
                jwtTokenResponse.StatusCode = 400;
                jwtTokenResponse.ErrorList = new List<JwtValidation>()
                {
                    new JwtValidation()
                    {
                        FieldName=nameof(audience),
                        Message="audience can't be blank"
                    }
                };
                return jwtTokenResponse;
            }

            if (expires != null && expires < issuedAt)
            {
                jwtTokenResponse.StatusCode = 400;
                jwtTokenResponse.ErrorList = new List<JwtValidation>()
                {
                    new JwtValidation()
                    {
                        FieldName=nameof(audience),
                        Message="expires time should be greater then current utc time"
                    }
                };
                return jwtTokenResponse;
            }

            string JwtToken = GenerateToken(claims, audience, expires, issuedAt);

            if (string.IsNullOrWhiteSpace(JwtToken))
            {
                jwtTokenResponse.StatusCode = 400;
                jwtTokenResponse.ErrorList = new List<JwtValidation>()
                {
                    new JwtValidation()
                    {
                        Message="Error while generating token"
                    }
                };
            }
            else
            {
                jwtTokenResponse.StatusCode = 200;
                jwtTokenResponse.ErrorList = null;
                jwtTokenResponse.TokenDetail = new JwtToken()
                {
                    Type = "Bearer",
                    Value = JwtToken,
                    CreatedAt = DateTime.UtcNow,
                    ExpiredAt = expires
                };
            }

            return jwtTokenResponse;
        }

        public string GenerateToken(Dictionary<string, string> claims, string audience, DateTime? expires, DateTime issuedAt)
        {
            string tokenString = string.Empty;
            try
            {
                Claim[] claimList = GetClaimsList(claims);

                //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
                var tokenHandler = new JwtSecurityTokenHandler();

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimList);

                //create the jwt
                var token = tokenHandler.CreateJwtSecurityToken(
                                        issuer: _config.Issuer,
                                        audience: audience,
                                        subject: claimsIdentity,
                                        notBefore: issuedAt,
                                        expires: expires,
                                        signingCredentials: _signingCredentials
                                        );

                tokenString = tokenHandler.WriteToken(token);
            }
            catch
            {
                tokenString = string.Empty;
            }
            return tokenString;
        }

        public Claim[] GetClaimsList(Dictionary<string, string> claimsList)
        {
            List<Claim> claims = new List<Claim>();
            if (claimsList != null)
            {
                foreach (KeyValuePair<string, string> kv in claimsList)
                {
                    claims.Add(new Claim(kv.Key, kv.Value));
                }
            }
            return claims.ToArray();
        }
    }
}
