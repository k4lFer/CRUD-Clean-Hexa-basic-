using Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Shared.Message;
using Domain.Entities;
using Application.DTOs.Customer;
using Application.DTOs.Common;

namespace Application.Features.Customer.Queries.GetById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<CustomerResponseDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Result<CustomerResponseDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            TCustomer? customer = await _customerRepository.GetByIdAsync(request.id, cancellationToken);

            if (customer != null)
            {
                var customerDto = _mapper.Map<CustomerResponseDto>(customer);
                return Result<CustomerResponseDto>.Success(customerDto);
            }
            return Result<CustomerResponseDto>.NotFound("Customer not found");
        }
    }

}