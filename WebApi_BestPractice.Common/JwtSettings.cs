namespace WebApi_BestPractice.Common
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public double ExpireMinutes { get; set; }
        public double NotBeforeMinutes { get; set; }
    }
}
