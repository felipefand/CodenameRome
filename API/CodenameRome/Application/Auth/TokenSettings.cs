namespace CodenameRome.Auth
{
    public class TokenSettings
    {
        public string? Secret { get; set; }
        public string? Issuer { get; set; }
        public int ExpirationTimeInMinutes { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }
        public string? Audience { get; set; }
    }
}
