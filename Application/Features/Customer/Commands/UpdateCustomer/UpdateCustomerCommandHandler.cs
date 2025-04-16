using Application.Common.Interfaces;
using Application.DTOs.Common;
using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Message;

namespace Application.Features.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<object>>
    {
        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _repository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<object>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
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
                    return Result<object>.Warning("No se realizaron cambios.");
                }
                await _repository.UpdateAsync(existingCustomer, cancellationToken); // Actualizar el cliente
                await _unitOfWork.SaveChangesAsync(cancellationToken); // Guardar cambios en la base de datos
                return Result<object>.Success("Cliente actualizado exitosamente.");
            }
            return Result<object>.NotFound("Cliente no encontrado.");
        }
    }
}