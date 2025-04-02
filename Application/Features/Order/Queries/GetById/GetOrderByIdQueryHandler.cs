using Application.DTOs.Order;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Message;

namespace Application.Features.Order.Queries.GetById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, (Message, OrderResponseDto)>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<(Message, OrderResponseDto)> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var message = new Message();
            var order = await _orderRepository.GetOrder(request.OrderId, cancellationToken);
           
            if(order == null)
            {
                message.NotFound();
                message.AddMessage("Order not found.");
                return (message, null);
            }
            var orderDto = _mapper.Map<OrderResponseDto>(order);
            message.Success();
            return (message, orderDto);
        }
    }
}