using Application.DTOs.Common;
using Application.DTOs.Customer;
using Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Shared.Message;

namespace Application.Features.Customer.Queries.GetAllPag
{
    public class GetAllCustomersPagQueryHandler : IRequestHandler<GetAllCustomersPagQuery, Result<PagedResponse<CustomerResponseDto>>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersPagQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedResponse<CustomerResponseDto>>> Handle(GetAllCustomersPagQuery request, CancellationToken cancellationToken)
        {
            var pagedCustomers = await _customerRepository.GetAllPaged(
                request.pageNumber, 
                request.pageSize, 
                request.seach, 
                cancellationToken);

            if (pagedCustomers != null)
            {
                var customers = _mapper.Map<IEnumerable<CustomerResponseDto>>(pagedCustomers.Items);

                var response = PagedResponse<CustomerResponseDto>.Ok(
                    new PaginationResponseDto<CustomerResponseDto>(customers, pagedCustomers.TotalCount, request.pageNumber, request.pageSize)
                );

                return Result<PagedResponse<CustomerResponseDto>>.Success(response);
            }
            return Result<PagedResponse<CustomerResponseDto>>.NotFound("No customers found.");
        }
    }
}
