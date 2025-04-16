using Domain.Entities;
using MediatR;
using Domain.Interfaces.Repositories;
using Application.Common.Interfaces;
using Application.DTOs.Common;

namespace Application.Features.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDomainEventDispatcher _dispatcher;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IDomainEventDispatcher dispatcher, ICustomerRepository customerRepository) 
            => (_unitOfWork, _dispatcher, _customerRepository) = (unitOfWork, dispatcher, customerRepository);

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="request">The request containing the customer information</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the operation, including the created resource</returns>
        public async Task<Result<object>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = TCustomer.Create(
                request.Customer.firstName,
                request.Customer.lastName,
                request.Customer.documentType,
                request.Customer.documentNumber,
                request.Customer.email,
                request.Customer.phoneNumber
            );

            await _customerRepository.AddAsync(customer, cancellationToken);

            //await _unitOfWork.CommitAsync(cancellationToken); // 🔹 Guarda los cambios en la BD
            await _unitOfWork.SaveChangesAsync(cancellationToken); // 🔹 Guarda los cambios en la BD

            await _dispatcher.DispatchAndClearEventsAsync(customer, cancellationToken); 

            return Result<object>.Created( "Customer created successfully.");
        }
    }
}
