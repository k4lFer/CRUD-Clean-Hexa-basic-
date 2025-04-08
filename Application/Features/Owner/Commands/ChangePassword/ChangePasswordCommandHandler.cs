using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Message;
using Application.Common.Interfaces;

namespace Application.Features.Owner.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Message>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOwnerRepository _ownerRepository;
        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IOwnerRepository ownerRepository)
        => (_unitOfWork, _ownerRepository) = (unitOfWork, ownerRepository);

        public async Task<Message> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var message = new Message();

            var owner = await _ownerRepository.GetByIdAsync(request.Id, cancellationToken);
            if (owner != null)
            {
                owner.UpdatePassword(BCrypt.Net.BCrypt.HashPassword(request.NewPassword));

                await _ownerRepository.UpdateAsync(owner, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                message.Updated();
                message.AddMessage("Contraseña actualizada con éxito");
                return message;
            }

            message.Error();
            message.AddMessage("Propietario no encontrado");
            return message;
        }
    }
}