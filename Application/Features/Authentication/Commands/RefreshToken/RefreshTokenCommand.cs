using Application.DTOs.Auth;
using MediatR;
using Shared.Message;

namespace Application.Features.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<(Message, AuthResponseDto)>
    {
        public string Token { get; set; }
        public RefreshTokenCommand(string token)
        {
            Token = token;
        }
    }
}