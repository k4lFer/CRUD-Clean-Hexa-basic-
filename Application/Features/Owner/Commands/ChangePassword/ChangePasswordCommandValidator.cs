using Domain.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Owner.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        private readonly IOwnerRepository  _ownerRepository;
        
        public ChangePasswordCommandValidator(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
            
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El ID del propietario es requerido")
                .MustAsync(async (id, cancellation) => await _ownerRepository.GetByIdAsync(id, cancellation) != null)
                .WithMessage("El propietario no existe");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña actual es requerida");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("La contraseña nueva es requerida");

            RuleFor(x => x.NewPassword)
                .MinimumLength(6).WithMessage("La contraseña nueva debe tener al menos 6 caracteres")
                .Matches(@"[A-Z]").WithMessage("La contraseña nueva debe contener al menos una letra mayúscula")
                .Matches(@"[a-z]").WithMessage("La contraseña nueva debe contener al menos una letra minúscula")
                .Matches(@"[0-9]").WithMessage("La contraseña nueva debe contener al menos un número")
                .Matches(@"[\W_]").WithMessage("La contraseña nueva debe contener al menos un carácter especial");
            
            RuleFor(x => x)
                .MustAsync(async (request, cancellation) =>
                    {
                        var owner = await _ownerRepository.GetByIdAsync(request.Id, cancellation);
                        return owner != null && !BCrypt.Net.BCrypt.Verify(request.NewPassword, owner.password);
                    })
                .WithMessage("La nueva contraseña no puede ser igual a la anterior");

            RuleFor(x => x)
                .MustAsync(async (request, cancellation) =>
                {
                    var owner = await _ownerRepository.GetByIdAsync(request.Id, cancellation);
                    return owner != null && BCrypt.Net.BCrypt.Verify(request.Password, owner.password);
                })
                .WithMessage("La contraseña actual es incorrecta");
            
        }
    }
}