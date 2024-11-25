namespace Core.Auth
{
    public class AuthProperties
    {
        public static string SecretKey { get; set; } = string.Empty;
        public static string Audience { get; set; } = string.Empty;
        public static string Issuer { get; set; } = string.Empty;
        public static int ExpirationTime { get; set; }
    }
}