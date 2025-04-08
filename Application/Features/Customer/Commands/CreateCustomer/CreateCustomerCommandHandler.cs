using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Shared.Message;
using Application.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Application.Common.Interfaces;

namespace Application.Features.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Message>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDomainEventDispatcher _dispatcher;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IDomainEventDispatcher dispatcher, ICustomerRepository customerRepository) 
            => (_unitOfWork, _dispatcher, _customerRepository) = (unitOfWork, dispatcher, customerRepository);

        public async Task<Message> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var message = new Message();
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

            message.Created();
            message.AddMessage("Cliente creado exitosamente.");
            return message;
        }
    }
}
