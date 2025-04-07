using System.Security.Claims;
using Application.DTOs.Owner;
using Domain.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Application.Common.Interfaces;

namespace Application.Features.Owner.Queries.GetProfile
{
    public class GetOwnerQueryHandler : IRequestHandler<GetOwnerQuery, OwnerResponseDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetOwnerQueryHandler> _logger;

        public GetOwnerQueryHandler(IOwnerRepository ownerRepository, IMapper mapper, ILogger<GetOwnerQueryHandler> logger, ICurrentUserService currentUserService)
        {

            _ownerRepository = ownerRepository;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
        }
        public async Task<OwnerResponseDto> Handle(GetOwnerQuery request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetByIdAsync(Guid.Parse(_currentUserService.UserId!), cancellationToken);
            return _mapper.Map<OwnerResponseDto>(owner);     
        }
    }
}