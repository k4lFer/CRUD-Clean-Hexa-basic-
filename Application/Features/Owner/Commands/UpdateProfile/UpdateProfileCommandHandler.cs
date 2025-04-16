using Application.Common.Interfaces;
using Application.DTOs.Common;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Owner.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOwnerRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateProfileCommandHandler(IUnitOfWork unitOfWork, IOwnerRepository repository, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _currentUserService = currentUserService;
        }
        public async Task<Result<object>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var exist = await _repository.GetByIdAsync(Guid.Parse(_currentUserService.UserId!), cancellationToken);
            if (exist != null)
            {
                var originalValues = new 
                {
                    exist.username,
                    exist.email,
                    exist.firstName,
                    exist.lastName,
                    exist.dni,
                    exist.ruc,
                    exist.address,
                    exist.phoneNumber
                };

                // Aplicar actualización usando el método de la entidad
                exist.UpdateProfile(
                    request.Owner.Username,
                    request.Owner.Email,
                    request.Owner.FirstName,
                    request.Owner.LastName,
                    request.Owner.Dni,
                    request.Owner.Ruc,
                    request.Owner.Address,
                    request.Owner.PhoneNumber
                );

                if(exist.username == originalValues.username &&
                    exist.email == originalValues.email &&
                    exist.firstName == originalValues.firstName &&
                    exist.lastName == originalValues.lastName &&
                    exist.dni == originalValues.dni &&
                    exist.ruc == originalValues.ruc &&
                    exist.address == originalValues.address &&
                    exist.phoneNumber == originalValues.phoneNumber)
                {
                    return Result<object>.Warning("No se realizaron cambios.");
                }
                await _repository.UpdateAsync(exist, cancellationToken); // 🔹 Actualiza el propietario en la BD
                await _unitOfWork.SaveChangesAsync(cancellationToken); // 🔹 Guarda los cambios en la BD
                return Result<object>.Success(null, "Perfil actualizado con éxito.");                  
                
            }
            return Result<object>.Error("No se encontró el propietario.");
            
        }
    }
}
