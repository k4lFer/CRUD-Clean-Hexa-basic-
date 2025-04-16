using Application.DTOs.Common;
using Application.Interfaces.Services;
using MediatR;
using Shared.Message;

namespace Application.Features.Order.Queries.CancelOrder
{
    public class CancelOrderQueryHandler : IRequestHandler<CancelOrderQuery, Result<object>>
    {
        private readonly IOrderService _orderService;
        public CancelOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<Result<object>> Handle(CancelOrderQuery request, CancellationToken cancellationToken)
        {
            return await _orderService.CancelOrderAsync(request.OrderId, cancellationToken);
        }
    }
}