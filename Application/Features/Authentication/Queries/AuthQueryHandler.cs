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
    public class AuthQueryHandler : IRequestHandler<AuthQuery, Result<AuthResponseDto>>
    {
        private readonly ITokenUtilService _jwtService;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public AuthQueryHandler(ITokenUtilService jwtService, IOwnerRepository ownerRepository, IMapper mapper) 
        => (_jwtService, _ownerRepository, _mapper) = (jwtService, ownerRepository, mapper);

        public async Task<Result<AuthResponseDto>> Handle(AuthQuery request, CancellationToken cancellationToken)
        {
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
                    return Result<AuthResponseDto>.Success(authDto, "Credenciales correctas. Bienvenido al sistema!.");
                }
                return Result<AuthResponseDto>.Conflict("Credenciales incorrectas (La contraseña no coincide con el usuario).");

            }
            return Result<AuthResponseDto>.Error("Credenciales incorrectas (El usuario no existe).");
        }

    }
}