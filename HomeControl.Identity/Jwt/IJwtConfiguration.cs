namespace HomeControl.Identity.Jwt
{
    public interface IJwtConfiguration
    {
        string Audience { get; set; }
        string Issuer { get; set; }
        int MinutesToExpire { get; set; }
        string SecretKey { get; set; }
    }
}