using Application.DTOs.Common;
using Application.DTOs.Owner;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Message;

namespace Application.Features.Owner.Queries.GetById
{
    public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, Result<OwnerResponseDto>>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        public GetOwnerByIdQueryHandler(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }
        public async Task<Result<OwnerResponseDto>> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetByIdAsync(request.Id, cancellationToken);
            if(owner != null)
            {
                var ownerDto = _mapper.Map<OwnerResponseDto>(owner);
                return Result<OwnerResponseDto>.Success(ownerDto); // 🔹 Retorna el resultado
            }
            return Result<OwnerResponseDto>.NotFound("Propietario no encontrado."); // 🔹 Retorna el resultado
        }
    }
}