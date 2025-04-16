using Application.DTOs.Auth;
using Application.DTOs.Common;
using Application.Interfaces.Services;
using MediatR;
using Shared.Message;

namespace Application.Features.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResponseDto>>
    {
        private readonly ITokenUtilService _jwtService;
        public RefreshTokenCommandHandler(ITokenUtilService jwtService) 
            => _jwtService = jwtService;

        public async Task<Result<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return Result<AuthResponseDto>.Error("Token no puede ser nulo.");
            }
            var result = await _jwtService.GenerateAccessTokenFromRefreshToken(request.Token);
            if(result.IsSuccess)
            {
                AuthResponseDto user = new() { 
                    tokens = new Tokens
                    {
                        accessToken = result.Data.accessToken,
                    }
                };
                return Result<AuthResponseDto>.Success(user);
            }
            return Result<AuthResponseDto>.Error(result.Messages.FirstOrDefault()?.Message.FirstOrDefault() ?? "Error desconocido");
        }
    }
}