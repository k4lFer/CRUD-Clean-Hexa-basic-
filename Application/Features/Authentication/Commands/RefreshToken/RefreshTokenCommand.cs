using Application.DTOs.Auth;
using Application.DTOs.Common;
using MediatR;
using Shared.Message;

namespace Application.Features.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<Result<AuthResponseDto>>
    {
        public string Token { get; set; }
        public RefreshTokenCommand(string token)
        {
            Token = token;
        }
    }
}