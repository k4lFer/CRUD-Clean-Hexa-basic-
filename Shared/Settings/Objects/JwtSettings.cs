namespace Shared.Settings.Objects
{
    public class JwtSettings
    {
        public string? Audience { get; set; }
        public string? Issuer { get; set; }
        // Llaves de encriptación para accesstoken y refreshtoken (2 diferentes)
        public string? AccessTokenJwtKey { get; set; }
        public string? RefreshTokenJwtKey { get; set; }
    }
}