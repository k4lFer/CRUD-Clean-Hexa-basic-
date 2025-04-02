using Application.DTOs.Auth;
using Application.DTOs.Common;
using Domain.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared.Message;

namespace Application.Features.Authentication.Queries
{
    public class AuthQueryHandler : IRequestHandler<AuthQuery, (Message, AuthResponseDto)>
    {
        private readonly ITokenUtilService _jwtService;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public AuthQueryHandler(ITokenUtilService jwtService, IOwnerRepository ownerRepository, IMapper mapper) 
        => (_jwtService, _ownerRepository, _mapper) = (jwtService, ownerRepository, mapper);

        public async Task<(Message, AuthResponseDto)> Handle(AuthQuery request, CancellationToken cancellationToken)
        {
            var message = new Message();
            TOwner? owner = await _ownerRepository.GetByUsername(request.AuthDto.username, cancellationToken);

            if (owner != null)
            {
                if (BCrypt.Net.BCrypt.Verify(request.AuthDto.password, owner.password))
                {
                    var authDto = _mapper.Map<AuthResponseDto>(owner);

                    var accessToken = await _jwtService.GenerateAccessToken(authDto);
                    var refreshToken = await _jwtService.GenerateRefreshToken(authDto);

                    authDto.tokens = new Tokens
                    {
                        accessToken = accessToken,
                        refreshToken = refreshToken,
                    };

                    message.AddMessage("Credenciales correctas. Bienvenido al sistema!.");    
                    message.Success();
                    return (message, authDto);
                }

                message.AddMessage("Credenciales incorrectas (La contraseña no coincide con el usuario).");
                return (message, null);
            }

            message.Error();
            message.AddMessage("Credenciales incorrectas.");
            return (message, null);
        }

    }
}