using Application.DTOs.Auth;
using Application.DTOs.Common;
using Shared.Message;

namespace Application.Interfaces.Services
{
    public interface ITokenUtilService
    {
        Task<string> GenerateAccessToken(AuthResponseDto user);
        Task<string> GenerateRefreshToken(AuthResponseDto user);
        Task<Result<Tokens>> GenerateAccessTokenFromRefreshToken(string refreshToken);
    }
}
