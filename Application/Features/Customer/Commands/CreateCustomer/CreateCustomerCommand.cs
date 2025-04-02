using Application.DTOs.Customer;
using MediatR;
using Shared.Message;

namespace Application.Features.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Message>
    {
        public CustomerCreateDto Customer { get; set; }
        public CreateCustomerCommand(CustomerCreateDto customer) => Customer = customer;
    }
}