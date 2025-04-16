using Application.DTOs.Auth;
using Application.DTOs.Common;
using MediatR;
using Shared.Message;

namespace Application.Features.Authentication.Queries
{
    public class AuthQuery : IRequest<Result<AuthResponseDto>>
    {
        public AuthDto AuthDto { get; set; }
        public AuthQuery(AuthDto authDto) => AuthDto = authDto;
    }
}