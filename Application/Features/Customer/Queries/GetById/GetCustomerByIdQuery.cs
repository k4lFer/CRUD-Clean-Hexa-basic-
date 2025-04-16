using Application.DTOs.Common;
using Application.DTOs.Customer;
using MediatR;
using Shared.Message;

namespace Application.Features.Customer.Queries.GetById
{
    public class GetCustomerByIdQuery : IRequest<Result<CustomerResponseDto>>
    {
        public Guid id { get; set; }

        public GetCustomerByIdQuery(Guid Id)
        {
            id = Id;
        }
    }

}