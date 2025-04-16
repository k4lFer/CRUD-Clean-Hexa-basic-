using Application.DTOs.Common;
using Application.DTOs.Order;
using MediatR;
using Shared.Message;

namespace Application.Features.Order.Queries.GetById
{
    public class GetOrderByIdQuery : IRequest<Result<OrderResponseDto>>
    {
        public Guid OrderId { get; set; }

        public GetOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}