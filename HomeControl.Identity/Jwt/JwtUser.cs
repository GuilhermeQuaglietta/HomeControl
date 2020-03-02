using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace HomeControl.Identity.Jwt
{
    public class JwtUser
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public ClaimsPrincipal Claims { get; private set; }
        public string Token { get; private set; }
        public SecurityToken SecurityToken { get; private set; }

        public JwtUser(ClaimsPrincipal claims, string token, SecurityToken securityToken)
        {
            Claims = claims;
            Token = token;
            SecurityToken = securityToken;

            Id = int.Parse(claims.FindFirst(JwtClaimTypes.Id).Value);
            Name = claims.FindFirst(JwtClaimTypes.Name).Value;
            Email = claims.FindFirst(JwtClaimTypes.Email).Value;
        }
    }
}
