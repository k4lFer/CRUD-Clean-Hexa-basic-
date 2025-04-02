using Application.DTOs.Auth;
using Application.DTOs.Common;
using Shared.Message;

namespace Application.Interfaces.Services
{
    public interface ITokenUtilService
    {
        Task<string> GenerateAccessToken(AuthResponseDto user);
        string GetUserIdFromAccessToken(string accessToken);
        Task<string> GenerateRefreshToken(AuthResponseDto user);
        Task<(Tokens, Message)> GenerateAccessTokenFromRefreshToken(string refreshToken);
    }
}
