using Application.DTOs.Customer;
using Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Shared.Message;

namespace Application.Features.Customer.Queries.GetAll
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, (Message, IEnumerable<CustomerResponseDto>)>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<(Message, IEnumerable<CustomerResponseDto>)> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var message = new Message();
            var customers = await _customerRepository.GetAllAsync(cancellationToken);

            if (!customers.Any())
            {
                message.NotFound();
                message.AddMessage("No hay clientes registrados.");
                return (message, Enumerable.Empty<CustomerResponseDto>());
            }

            var CustomerResponseDtos = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
            message.Success();
            return (message, CustomerResponseDtos);
        }
    }
}
