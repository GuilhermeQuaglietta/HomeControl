namespace HomeControl.AccessControl.WebApi.Infrastructure.Settings
{
    public class CorsSettings
    {
        public const string OptionsName = "CorsSettings";
        public const string PolicyName = "AllowPrivateOrigins";

        public string[] AllowedOrigins { get; set; }
        public string[] AllowedHeaders { get; set; }
        public string[] AllowedMethods { get; set; }
    }
}
