using Application.DTOs.Owner;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Message;

namespace Application.Features.Owner.Queries.GetById
{
    public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, (Message, OwnerResponseDto)>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        public GetOwnerByIdQueryHandler(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }
        public async Task<(Message, OwnerResponseDto)> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetByIdAsync(request.Id, cancellationToken);
            var message = new Message();
            if(owner != null)
            {
                message.NotFound();
                message.AddMessage("Propietario no encontrado.");
                return (message, null);
            }
            var ownerDto = _mapper.Map<OwnerResponseDto>(owner);
            message.Success();
            return (message, ownerDto);
        }
    }
}