using Application.DTOs.Order;
using MediatR;
using Shared.Message;

namespace Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Message>
    {
        public OrderCreateDto Order { get; set; }
        public CreateOrderCommand(OrderCreateDto order) => Order = order;
    }
}