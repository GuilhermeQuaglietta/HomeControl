namespace HomeControl.Identity.Jwt
{
    public interface IJwtHandler
    {
        string GenerateToken(IJwtConfiguration configuration, int id, string name, string email);
        JwtValidationResult VerifyToken(IJwtConfiguration configuration, string token);
    }
}
