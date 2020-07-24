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

    public class IdentitySettings
    {
        public bool PasswordRequireDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireNonAlphanumic { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool RequireUniqueEmail { get; set; }
    }

    public class SiteSettings
    {
        public IdentitySettings IdentitySettings { get; set; }
        public JwtSettings jwtSettings { get; set; }
    }
}
