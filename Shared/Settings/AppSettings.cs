using Microsoft.Extensions.Configuration;

namespace Shared.Settings
{
    public static class AppSettings
    {
        private static IConfiguration? _configuration;
        public static GlobalSettings _globalSettings = new();

        public static void Init(IConfiguration configuration)
        {
            _configuration = configuration;

            _globalSettings.dbSettings.ConnectionStringMySql = configuration["ConnectionStrings:MySQLConnection"];
            _globalSettings.jwtSettings.AccessTokenJwtKey = configuration["Authentication:Jwt:AccessTokenSecret"];
            _globalSettings.jwtSettings.RefreshTokenJwtKey = configuration["Authentication:Jwt:RefreshTokenSecret"];
            _globalSettings.jwtSettings.Issuer = configuration["Authentication:Jwt:Issuer"];
            _globalSettings.jwtSettings.Audience = configuration["Authentication:Jwt:Audience"];
            _globalSettings.corsSettings.OriginRequest = configuration["Cors:originRequest"];
        }

        public static string GetConnectionStringMySql() => _globalSettings.dbSettings.ConnectionStringMySql;
        public static string GetAccessJwtSecret() => _globalSettings.jwtSettings.AccessTokenJwtKey;
        public static string GetRefreshJwtSecret() => _globalSettings.jwtSettings.RefreshTokenJwtKey;
        public static string GetIssuer() => _globalSettings.jwtSettings.Issuer;
        public static string GetAudience() => _globalSettings.jwtSettings.Audience;
        public static string GetOriginRequest() => _globalSettings.corsSettings.OriginRequest;
    }

}
