using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HomeControl.Identity.Jwt
{

    public class JwtSymmetricHandler : IJwtHandler
    {
        public JwtSymmetricHandler()
        {

        }

        public string GenerateToken(IJwtConfiguration configuration, string userName)
        {
            byte[] byteKey = Convert.FromBase64String(configuration.SecretKey);
            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity identity = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.UserName, userName),
                }
            );

            var now = DateTime.Now;

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = configuration.Issuer,
                Audience = configuration.Audience,
                Expires = now.AddHours(configuration.HoursToExpire),
                NotBefore = now,
                IssuedAt = now,
                SigningCredentials = credentials,
            });

            return handler.WriteToken(securityToken);
        }

        public string GenerateToken(IJwtConfiguration configuration, int id, string name, string email)
        {
            throw new NotImplementedException();
        }

        public JwtValidationResult VerifyToken(IJwtConfiguration configuration, string token)
        {
            var byteKey = Convert.FromBase64String(configuration.SecretKey);
            var securityKey = new SymmetricSecurityKey(byteKey);

            var parameters = new TokenValidationParameters
            {
                ValidIssuer = configuration.Issuer,
                ValidAudience = configuration.Audience,
                IssuerSigningKey = securityKey,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, parameters, out var parsedToken);
                var identity = new JwtIdentity(claimsPrincipal, token, parsedToken);
                return new JwtValidationResult(new JwtUser(identity.Claims, identity.Token, identity.SecurityToken));
            }
            catch (Exception e)
            {
                return new JwtValidationResult(JwtValidationResultCode.InvalidToken, e);
            }
        }
    }
}
