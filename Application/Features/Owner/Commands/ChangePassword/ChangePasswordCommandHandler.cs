using Domain.Interfaces.Repositories;
using Application.Interfaces.Services;
using BCrypt.Net;
using MediatR;
using Shared.Message;

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
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var owner = await _ownerRepository.GetByIdAsync(request.Id, cancellationToken);
                if (owner == null)
                {
                    message.Error();
                    message.AddMessage("Propietario no encontrado");
                    return message;
                }
                if (!BCrypt.Net.BCrypt.Verify(request.Password, owner.password))
                {
                    message.Error();
                    message.AddMessage("Contraseña incorrecta");
                    return message;
                }
                if (BCrypt.Net.BCrypt.Verify(request.NewPassword, owner.password))
                {
                    message.Error();
                    message.AddMessage("La nueva contraseña no puede ser igual a la anterior");
                    return message;
                }
                owner.UpdatePassword(BCrypt.Net.BCrypt.HashPassword(request.NewPassword));
                await _ownerRepository.UpdateAsync(owner, cancellationToken);
                
                await _unitOfWork.CommitAsync(cancellationToken);
                message.Updated();
                message.AddMessage("Contraseña actualizada con éxito");
                return message;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                message.Error();
                message.AddMessage($"Error al cambiar la contraseña: {ex.Message}");
                message.AddMessage($"Error al cambiar la contraseña: {ex.Message}");
                return message;
            }
        }
    }
}