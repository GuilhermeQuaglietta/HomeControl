namespace HomeControl.Identity.Jwt
{
    public interface IJwtHandler
    {
        string GenerateToken(IJwtConfiguration configuration, string userName);
        JwtValidationResult VerifyToken(IJwtConfiguration configuration, string token);
    }
}
