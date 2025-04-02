using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Application.DTOs.Common;
using Application.Interfaces.Services;
using Shared.Message;
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
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Task.FromResult(jwt);
        }

        public Task<(Tokens, Message)> GenerateAccessTokenFromRefreshToken(string refreshToken)
        {
            var message = new Message();
            var tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey refreshKey = new(Encoding.UTF8.GetBytes(AppSettings.GetRefreshJwtSecret()));

            try
            {
                var principal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = refreshKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken securityToken);

                var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var role = principal.FindFirst(ClaimTypes.Role)?.Value ?? "";

                SymmetricSecurityKey accessKey = new(Encoding.UTF8.GetBytes(AppSettings.GetAccessJwtSecret()));
                List<Claim> newClaims =
                [
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Role, role)
                ];

                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(newClaims),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(accessKey, SecurityAlgorithms.HmacSha256)
                };

                var newToken = tokenHandler.CreateToken(descriptor);
                var newAccessToken = tokenHandler.WriteToken(newToken);

                message.AddMessage("Access Token generado exitosamente.");
                message.Success();

                return Task.FromResult((new Tokens
                {
                    accessToken = newAccessToken
                }, message));
            }
            catch (SecurityTokenExpiredException)
            {
                message.AddMessage("El refresh token ha expirado.");
                message.Error();
                return Task.FromResult<(Tokens, Message)>((null, message));
            }
            catch (Exception ex)
            {
                message.AddMessage("Error al validar el refresh token: " + ex.Message);
                message.Error();
                return Task.FromResult<(Tokens, Message)>((null, message));
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
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Task.FromResult(jwt);
        }

        public string GetUserIdFromAccessToken(string accessToken)
        {
            String token = accessToken.Replace("Bearer ", "");
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken? jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            Claim? accessClaim = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            return accessClaim?.Value ?? string.Empty;
        }
    }
}
