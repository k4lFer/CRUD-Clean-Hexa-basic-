using Application.Common.Interfaces;
using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Message;

namespace Application.Features.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Message>
    {
        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _repository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Message> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var message = new Message();
            var existingCustomer = await _repository.GetByIdAsync(request.Customer.id, cancellationToken);
            if (existingCustomer != null)
            {
                var originalValues = new 
                {
                    existingCustomer.firstName,
                    existingCustomer.lastName,
                    existingCustomer.documentType,
                    existingCustomer.documentNumber,
                    existingCustomer.email,
                    existingCustomer.phoneNumber
                };

                // Aplicar actualización usando el método de la entidad
                existingCustomer.Update(
                    request.Customer.firstName,
                    request.Customer.lastName,
                    request.Customer.documentType,
                    request.Customer.documentNumber,
                    request.Customer.email,
                    request.Customer.phoneNumber
                );

                    // Verificar si hubo cambios reales
                if (existingCustomer.firstName == originalValues.firstName &&
                    existingCustomer.lastName == originalValues.lastName &&
                    existingCustomer.documentType == originalValues.documentType &&
                    existingCustomer.documentNumber == originalValues.documentNumber &&
                    existingCustomer.email == originalValues.email &&
                    existingCustomer.phoneNumber == originalValues.phoneNumber)
                {
                    message.AddMessage("No se realizaron cambios.");
                    message.Warning();
                    return message;
                }
                await _repository.UpdateAsync(existingCustomer, cancellationToken); // Actualizar el cliente
                await _unitOfWork.SaveChangesAsync(cancellationToken); // Guardar cambios en la base de datos

                message.Success();
                message.AddMessage("Cliente actualizado exitosamente.");
                return message;
            }

            message.AddMessage("Cliente no encontrado.");
            message.Error();
            return message;
     
        }
    }
}