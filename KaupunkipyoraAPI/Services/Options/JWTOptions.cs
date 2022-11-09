namespace KaupunkipyoraAPI.Services.Settings
{
    public class JWTOptions
    {
        public const string JWT = "JWT";
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpirationInMinutes { get; set; }
        public string SecretKey { get; set; } = string.Empty;
    }
}
