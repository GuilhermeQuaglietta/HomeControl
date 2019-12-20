namespace HomeControl.Identity.Jwt
{
    public class JwtConfiguration : IJwtConfiguration
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int MinutesToExpire { get; set; }
    }
}