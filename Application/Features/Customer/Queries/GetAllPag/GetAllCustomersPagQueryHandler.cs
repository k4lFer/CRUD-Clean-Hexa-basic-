using Application.DTOs.Common;
using Application.DTOs.Customer;
using Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Shared.Message;

namespace Application.Features.Customer.Queries.GetAllPag
{
    public class GetAllCustomersPagQueryHandler : IRequestHandler<GetAllCustomersPagQuery, (Message, PagedResponse<CustomerResponseDto>)>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersPagQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<(Message, PagedResponse<CustomerResponseDto>)> Handle(GetAllCustomersPagQuery request, CancellationToken cancellationToken)
        {
            var message = new Message();
            var pagedCustomers = await _customerRepository.GetAllPaged(
                request.pageNumber, 
                request.pageSize, 
                request.seach, 
                cancellationToken);

            if (pagedCustomers == null)
            {
                message.NotFound();
                message.AddMessage("Clientes no encontrados.");
                return (message, null);
            }
            
            var customers = _mapper.Map<IEnumerable<CustomerResponseDto>>(pagedCustomers.Items);

            var response = PagedResponse<CustomerResponseDto>.Ok(
                new PaginationResponseDto<CustomerResponseDto>(customers, pagedCustomers.TotalCount, request.pageNumber, request.pageSize)
            );

            message.Success();
            return (message, response);
        }
    }
}
