using Application.DTOs.Auth;
using Application.DTOs.Common;
using Application.Interfaces.Services;
using MediatR;
using Shared.Message;

namespace Application.Features.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, (Message, AuthResponseDto)>
    {
        private readonly ITokenUtilService _jwtService;
        public RefreshTokenCommandHandler(ITokenUtilService jwtService) 
            => _jwtService = jwtService;

        public async Task<(Message, AuthResponseDto)> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var message = new Message();
            if (string.IsNullOrEmpty(request.Token))
            {
                message.Error();
                message.AddMessage("Token no puede ser nulo.");
                return (message, (AuthResponseDto)null);
            }
            (var newToken, message) = await _jwtService.GenerateAccessTokenFromRefreshToken(request.Token);
            if(message.Type == "success")
            {
                AuthResponseDto user = new()
                {
                    tokens = new Tokens
                    {
                        accessToken = newToken.accessToken
                    }
                };
                message.Success();
                return (message, user);
            }
            return (message, (AuthResponseDto)null);
        }
    }
}