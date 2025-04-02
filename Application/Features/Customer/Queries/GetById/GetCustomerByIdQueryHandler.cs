using Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Shared.Message;
using Domain.Entities;
using Application.DTOs.Customer;

namespace Application.Features.Customer.Queries.GetById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, (Message, CustomerResponseDto)>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<(Message, CustomerResponseDto)> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var message = new Message();
            TCustomer? customer = await _customerRepository.GetByIdAsync(request.id, cancellationToken);

            if (customer == null)
            {
                message.NotFound();
                message.AddMessage("Cliente no encontrado.");
                return (message, null);
            }

            var customerDto = _mapper.Map<CustomerResponseDto>(customer);
            message.Success();
            return (message, customerDto);
        }
    }

}