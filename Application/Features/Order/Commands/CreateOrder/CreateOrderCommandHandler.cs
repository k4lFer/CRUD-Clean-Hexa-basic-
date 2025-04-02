using Application.Interfaces.Services;
using MediatR;
using Shared.Message;

namespace Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Message>
    {
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Message> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderService.CreateOrderAsync(request.Order, cancellationToken);
        }
    }
}