using Application.DTOs.Customer;
using MediatR;
using Shared.Message;

namespace Application.Features.Customer.Queries.GetAll
{
    public class GetAllCustomersQuery : IRequest<(Message, IEnumerable<CustomerResponseDto>)>
    {
        
    }
}