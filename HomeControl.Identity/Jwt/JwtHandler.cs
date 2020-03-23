using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HomeControl.Identity.Jwt
{
    public class JwtHandler : IJwtHandler
    {
        public string GenerateToken(IJwtConfiguration configuration, int id, string name, string email)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (id <= 0) throw new ArgumentException($"{nameof(id)} can't be less or equal to 0.", nameof(id));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException($"{nameof(name)} can't be null or empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException($"{nameof(email)} can't be null or empty.", nameof(email));

            //Throws exception on invalid configuration
            ValidateConfiguration(configuration);

            byte[] byteKey = Convert.FromBase64String(configuration.SecretKey);
            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity identity = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(JwtClaimTypes.Id, id.ToString()),
                    new Claim(JwtClaimTypes.Name, name),
                    new Claim(JwtClaimTypes.Email, email),
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

        public JwtValidationResult VerifyToken(IJwtConfiguration configuration, string token)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException($"{nameof(token)} can't be null or empty.", nameof(token));

            //Throws exception on invalid configuration
            ValidateConfiguration(configuration);

            try
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
                var claimsPrincipal = tokenHandler.ValidateToken(token, parameters, out var parsedToken);
                var identity = new JwtUser(claimsPrincipal, token, parsedToken);
                return new JwtValidationResult(identity);
            }
            catch (FormatException e)
            {
                return new JwtValidationResult(JwtValidationResultCode.MalFormedSecurityKey, e);
            }
            catch (SecurityTokenValidationException e)
            {
                return new JwtValidationResult(JwtValidationResultCode.InvalidToken, e);
            }
        }

        private void ValidateConfiguration(IJwtConfiguration configuration)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(configuration.Audience)) sb.AppendLine("Audience is empty.");
            if (string.IsNullOrWhiteSpace(configuration.Issuer)) sb.AppendLine("Issuer is empty.");
            if (string.IsNullOrWhiteSpace(configuration.SecretKey)) sb.AppendLine("SecretKey is empty.");

            if (sb.Length > 0) throw new InvalidOperationException($"Configuration is not valid: {sb.ToString()}");
        }
    }
}
