using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Message;
using Application.Common.Interfaces;
using Application.DTOs.Common;

namespace Application.Features.Owner.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOwnerRepository _ownerRepository;
        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IOwnerRepository ownerRepository)
        => (_unitOfWork, _ownerRepository) = (unitOfWork, ownerRepository);

        public async Task<Result<object>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetByIdAsync(request.Id, cancellationToken);
            if (owner != null)
            {
                owner.UpdatePassword(BCrypt.Net.BCrypt.HashPassword(request.NewPassword));

                await _ownerRepository.UpdateAsync(owner, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
                return  Result<object>.Success("Contraseña actualizada con éxito");
            }

            return Result<object>.Error("Propietario no encontrado");
        }
    }
}