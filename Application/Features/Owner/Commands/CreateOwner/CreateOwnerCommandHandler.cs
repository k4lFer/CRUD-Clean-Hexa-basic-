using Domain.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Shared.Message;
using Application.Common.Interfaces;

namespace Application.Features.Owner.Commands.CreateOwner
{
    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, Message>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOwnerRepository _ownerRepository;
        public CreateOwnerCommandHandler(IUnitOfWork unitOfWork, IOwnerRepository ownerRepository) 
        => (_unitOfWork, _ownerRepository) = (unitOfWork, ownerRepository);
        public async Task<Message> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            var message = new Message();
            TOwner owner = TOwner.Create(
                request.Owner.username,
                BCrypt.Net.BCrypt.HashPassword(request.Owner.password),
                request.Owner.email,
                request.Owner.firstName,
                request.Owner.lastName,
                request.Owner.dni,
                request.Owner.ruc,
                request.Owner.address,
                request.Owner.phoneNumber,
                request.Owner.role
            );

            await _ownerRepository.AddAsync(owner, cancellationToken); // 🔹 Agrega el propietario
            await _unitOfWork.SaveChangesAsync(cancellationToken); // 🔹 Guarda los cambios en la BD

            message.Created();
            message.AddMessage("Propietario creado con éxito");
            return message;
        }
    }
}