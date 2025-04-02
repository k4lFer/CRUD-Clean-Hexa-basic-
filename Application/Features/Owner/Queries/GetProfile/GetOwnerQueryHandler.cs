using System.Security.Claims;
using Application.DTOs.Owner;
using Domain.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Owner.Queries.GetProfile
{
    [Authorize]
    public class GetOwnerQueryHandler : IRequestHandler<GetOwnerQuery, OwnerProfile>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetOwnerQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetOwnerQueryHandler(IOwnerRepository ownerRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetOwnerQueryHandler> logger)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async Task<OwnerProfile> Handle(GetOwnerQuery request, CancellationToken cancellationToken)
        {
            var ownerId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(ownerId))
            {
                return null;
            }

            //_logger.LogInformation($"Extracted Owner ID: {ownerId}");
            var owner = await _ownerRepository.GetByIdAsync(Guid.Parse(ownerId!), cancellationToken);
            var ownerProfile = _mapper.Map<OwnerProfile>(owner);
            return ownerProfile;       
        }
    }

}