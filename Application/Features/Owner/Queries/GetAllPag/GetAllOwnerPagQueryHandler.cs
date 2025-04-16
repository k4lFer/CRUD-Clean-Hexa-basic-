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
    public class GetAllOwnerPagQueryHandler : IRequestHandler<GetAllOwnerPagQuery, Result<PagedResponse<OwnerResponseDto>>>
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
        public async Task<Result<PagedResponse<OwnerResponseDto>>> Handle(GetAllOwnerPagQuery request, CancellationToken cancellationToken)
        {
            var pagedOwners = await _ownerRepository.GetAllPaged(
                request.pageNumber, 
                request.pageSize, 
                request.search,
                Guid.Parse(_currentUserService.UserId!),
                cancellationToken);
            if (pagedOwners != null)
            {
                var owners = _mapper.Map<IEnumerable<OwnerResponseDto>>(pagedOwners.Items);
                var response = PagedResponse<OwnerResponseDto>.Ok(
                    new PaginationResponseDto<OwnerResponseDto>(owners, pagedOwners.TotalCount, request.pageNumber, request.pageSize)
                );
                return Result<PagedResponse<OwnerResponseDto>>.Success(response); // 🔹 Retorna el resultado  
            }
            return Result<PagedResponse<OwnerResponseDto>>.NotFound("Propietarios no encontrados.");
        }
    }
}