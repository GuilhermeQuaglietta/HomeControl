using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace HomeControl.Identity.Jwt
{
    public class JwtIdentity
    {
        public ClaimsPrincipal Claims { get; private set; }
        public string Token { get; private set; }
        public SecurityToken SecurityToken { get; private set; }

        public JwtIdentity(ClaimsPrincipal claims, string token, SecurityToken securityToken)
        {
            Claims = claims;
            Token = token;
            SecurityToken = securityToken;
        }
    }
}
