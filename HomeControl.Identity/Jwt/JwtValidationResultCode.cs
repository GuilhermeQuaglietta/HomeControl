namespace HomeControl.Identity.Jwt
{
    public enum JwtValidationResultCode
    {
        Ok = 1,
        MalFormedSecurityKey = 2,
        InvalidToken = 3,
        Unknown = 99,
    }
}
