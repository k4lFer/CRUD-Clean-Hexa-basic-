using Domain.Interfaces.Repositories;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Features.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        
        public UpdateCustomerCommandValidator(ICustomerRepository customerRepository) 
        { 
            _customerRepository = customerRepository;
            
            // Validación de que el objeto Customer no sea nulo
            RuleFor(x => x.Customer)
                .NotEmpty().WithMessage("Los datos del cliente son requeridos.")
                .DependentRules(() => 
                {
                    // Validación básica del ID (solo se ejecuta si Customer no es nulo)
                    RuleFor(x => x.Customer.id)
                        .NotEmpty().WithMessage("El ID del cliente es requerido.")
                        .MustAsync(ClientExistsAsync).WithMessage("El cliente no existe.");
                    
                    // Validación de formato de email si se proporciona
                    When(x => !string.IsNullOrEmpty(x.Customer.email), () =>
                    {
                        RuleFor(x => x.Customer.email)
                            .EmailAddress().WithMessage("El formato del email no es válido.")
                            .MustAsync(BeUniqueEmailIfChangedAsync).WithMessage("El correo electrónico ya está en uso.");
                    });

                    // Validación de documento si se proporciona
                    When(x => !string.IsNullOrEmpty(x.Customer.documentNumber), () =>
                    {
                        RuleFor(x => x.Customer.documentNumber)
                            .Length(5, 20).WithMessage("El número de documento debe tener entre 5 y 20 caracteres.")
                            .MustAsync(BeUniqueDocNumberIfChangedAsync).WithMessage("El número de documento ya está en uso.");
                    });

                    // Validación de teléfono si se proporciona
                    When(x => !string.IsNullOrEmpty(x.Customer.phoneNumber), () =>
                    {
                        RuleFor(x => x.Customer.phoneNumber)
                            .Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")
                            .WithMessage("El formato del número de teléfono no es válido.");
                    });
                });
        }

        // Método para validar que el cliente exista
        private async Task<bool> ClientExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetByIdAsync(id, cancellationToken) != null;
        }

        private async Task<bool> BeUniqueEmailIfChangedAsync(UpdateCustomerCommand command, string newEmail, CancellationToken cancellationToken)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(command.Customer.id, cancellationToken);
            
            // Si el email no ha cambiado o es nulo, es válido
            if (string.IsNullOrEmpty(newEmail) || existingCustomer?.email == newEmail)
                return true;
                
            return await _customerRepository.IsEmailUniqueAsync(newEmail, cancellationToken);
        }

        private async Task<bool> BeUniqueDocNumberIfChangedAsync(UpdateCustomerCommand command, string newDocNumber, CancellationToken cancellationToken)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(command.Customer.id);
            
            // Si el documento no ha cambiado o es nulo, es válido
            if (string.IsNullOrEmpty(newDocNumber) || existingCustomer?.documentNumber == newDocNumber)
                return true;
                
            return await _customerRepository.IsDocNumberUniqueAsync(newDocNumber, cancellationToken);
        }
    }
}