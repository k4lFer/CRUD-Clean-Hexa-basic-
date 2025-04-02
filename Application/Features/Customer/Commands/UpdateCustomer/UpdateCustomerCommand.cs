using Application.DTOs.Customer;
using MediatR;
using Shared.Message;

namespace Application.Features.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<Message>
    {
        public CustomerUpdateDto Customer { get; set; }
        public UpdateCustomerCommand(CustomerUpdateDto customer) => Customer = customer;
    }
}