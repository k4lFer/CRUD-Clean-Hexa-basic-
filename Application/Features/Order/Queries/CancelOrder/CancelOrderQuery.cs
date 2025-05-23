using Application.DTOs.Common;
using MediatR;
using Shared.Message;

namespace Application.Features.Order.Queries.CancelOrder
{
    public class CancelOrderQuery : IRequest<Result<object>>
    {
        public Guid OrderId { get; set; }
        public CancelOrderQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}