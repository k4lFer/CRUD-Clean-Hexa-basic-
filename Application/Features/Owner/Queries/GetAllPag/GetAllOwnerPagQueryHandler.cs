using System.Security.Claims;
using Application.DTOs.Common;
using Application.DTOs.Owner;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Shared.Message;

namespace Application.Features.Owner.Queries.GetAllPag
{
    [Authorize]
    public class GetAllOwnerPagQueryHandler : IRequestHandler<GetAllOwnerPagQuery, (Message, PagedResponse<OwnerResponseDto>)>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllOwnerPagQueryHandler(IOwnerRepository ownerRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<(Message, PagedResponse<OwnerResponseDto>)> Handle(GetAllOwnerPagQuery request, CancellationToken cancellationToken)
        {
            var ownerId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var message = new Message();
            var pagedOwners = await _ownerRepository.GetAllPaged(
                request.pageNumber, 
                request.pageSize, 
                request.search,
                Guid.Parse(ownerId),
                cancellationToken);
            if (pagedOwners == null)
            {
                message.NotFound();
                message.AddMessage("Propietarios no encontrados.");
                return (message, null);
            }
            var owners = _mapper.Map<IEnumerable<OwnerResponseDto>>(pagedOwners.Items);
            var response = PagedResponse<OwnerResponseDto>.Ok(
                new PaginationResponseDto<OwnerResponseDto>(owners, pagedOwners.TotalCount, request.pageNumber, request.pageSize)
            );
            message.Success();
            return (message, response);
            
        }
    }
}