using System.Security.Claims;
using Application.Common.Interfaces;
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
    public class GetAllOwnerPagQueryHandler : IRequestHandler<GetAllOwnerPagQuery, (Message, PagedResponse<OwnerResponseDto>)>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService; 

        public GetAllOwnerPagQueryHandler(IOwnerRepository ownerRepository, IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<(Message, PagedResponse<OwnerResponseDto>)> Handle(GetAllOwnerPagQuery request, CancellationToken cancellationToken)
        {
            var message = new Message();
            var ownerId = _currentUserService.UserId;

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