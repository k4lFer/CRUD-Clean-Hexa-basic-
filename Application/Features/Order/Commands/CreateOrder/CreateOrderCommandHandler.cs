using Application.DTOs.Common;
using Application.Interfaces.Services;
using MediatR;
using Shared.Message;

namespace Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<object>>
    {
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Result<object>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderService.CreateOrderAsync(request.Order, cancellationToken);
        }
    }
}