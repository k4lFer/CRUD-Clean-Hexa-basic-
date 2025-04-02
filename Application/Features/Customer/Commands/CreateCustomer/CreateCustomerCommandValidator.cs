using Domain.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Customer.Commands.CreateCustomer
{
     public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            RuleFor(x => x.Customer.firstName)
                .NotEmpty().WithMessage("El nombre es obligatorio.");

            RuleFor(x => x.Customer.lastName)
                .NotEmpty().WithMessage("El apellido es obligatorio.");

            RuleFor(x => x.Customer.email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("Debe ser un email válido.")
                .MustAsync(EmailUnique).WithMessage("El email ya está registrado.");

            RuleFor(x => x.Customer.phoneNumber)
                .NotEmpty().WithMessage("El teléfono es obligatorio.");

            RuleFor(x => x.Customer.documentNumber)
                .NotEmpty().WithMessage("El numero de documento es obligatorio.")
                .MustAsync(DocNumberUnique).WithMessage("El numero de documento ya está registrado.");
        }

        // Método para validar que el email sea único
        private async Task<bool> EmailUnique(string email, CancellationToken cancellationToken)
        {
           return await _customerRepository.IsDocNumberUniqueAsync(email, cancellationToken);
        }

        // Método para validar que el DNI sea único
        private async Task<bool> DocNumberUnique(string dni, CancellationToken cancellationToken)
        {
            return  await _customerRepository.IsEmailUniqueAsync(dni, cancellationToken);
        }
    }
}