using Application.DTOs.Order;
using MediatR;
using Shared.Message;

namespace Application.Features.Order.Queries.GetById
{
    public class GetOrderByIdQuery : IRequest<(Message, OrderResponseDto)>
    {
        public Guid OrderId { get; set; }

        public GetOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}