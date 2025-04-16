using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Application.DTOs.Common;
using Application.Interfaces.Services;
using Shared.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.DTOs.Auth;

namespace Infrastructure.Security
{
    public class JwtService : ITokenUtilService
    {
        public Task<string> GenerateAccessToken(AuthResponseDto user)
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(AppSettings.GetAccessJwtSecret()));

            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Role, user.role.ToString()),
            ];

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = AppSettings.GetIssuer(),
                Audience = AppSettings.GetAudience(),
                NotBefore = DateTime.UtcNow, 
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Task.FromResult(jwt);
        }

        public Task<Result<Tokens>> GenerateAccessTokenFromRefreshToken(string refreshToken)
        {
             var tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey refreshKey = new(Encoding.UTF8.GetBytes(AppSettings.GetRefreshJwtSecret()));

            try
            {
                var principal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = refreshKey,
                    ValidateIssuer = true,
                    ValidIssuer = AppSettings.GetIssuer(),
                    ValidateAudience = true,
                    ValidAudience = AppSettings.GetAudience(),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken securityToken);

                var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var role = principal.FindFirst(ClaimTypes.Role)?.Value ?? "";
                var type = principal.FindFirst("token_type")?.Value ?? "";

                if (type != "refresh")
                {
                    return Task.FromResult(Result<Tokens>.Error("El token no es un refresh token."));
                }

                SymmetricSecurityKey accessKey = new(Encoding.UTF8.GetBytes(AppSettings.GetAccessJwtSecret()));
                List<Claim> newClaims =
                [
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Role, role)
                ];

                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(newClaims),
                    Issuer = AppSettings.GetIssuer(),
                    Audience = AppSettings.GetAudience(),
                    NotBefore = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(accessKey, SecurityAlgorithms.HmacSha256)
                };

                var newToken = tokenHandler.CreateToken(descriptor);
                var newAccessToken = tokenHandler.WriteToken(newToken);

                return Task.FromResult(Result<Tokens>.Success(new Tokens
                {
                    accessToken = newAccessToken
                }, "Access Token generado exitosamente."));
            }
            catch (SecurityTokenExpiredException)
            {

                return Task.FromResult(Result<Tokens>.Error("El refresh token ha expirado."));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result<Tokens>.Exception("Error al validar el refresh token: " + ex.Message));
            }
        }

        public Task<string> GenerateRefreshToken(AuthResponseDto user)
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(AppSettings.GetRefreshJwtSecret()));
             List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Role, user.role.ToString()),
                new Claim("token_type", "refresh")
            ];
            var descriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Issuer = AppSettings.GetIssuer(),
                Audience = AppSettings.GetAudience(),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Task.FromResult(jwt);
        }

    }
}
