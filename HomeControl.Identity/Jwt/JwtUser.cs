using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace HomeControl.Identity.Jwt
{
    public class JwtUser
    {
        public static JwtIdentity Current { get; private set; }

        protected JwtUser()
        {

        }

        public static void SetIdentity(JwtIdentity identity)
        {
            Current = identity;
        }
        public static void SetIdentity(ClaimsPrincipal claims, string token, SecurityToken securityToken)
        {
            Current = new JwtIdentity(claims, token, securityToken);
        }
    }
}
