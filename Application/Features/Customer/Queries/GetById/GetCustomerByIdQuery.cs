using Application.DTOs.Customer;
using MediatR;
using Shared.Message;

namespace Application.Features.Customer.Queries.GetById
{
    public class GetCustomerByIdQuery : IRequest<(Message, CustomerResponseDto)>
    {
        public Guid id { get; set; }

        public GetCustomerByIdQuery(Guid Id)
        {
            id = Id;
        }
    }

}