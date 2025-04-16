using Application.DTOs.Common;
using Application.DTOs.Order;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Message;

namespace Application.Features.Order.Queries.GetById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderResponseDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<Result<OrderResponseDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrder(request.OrderId, cancellationToken);
           
            if(order != null)
            {
                var orderDto = _mapper.Map<OrderResponseDto>(order);
                return Result<OrderResponseDto>.Success(orderDto);
            }
            return Result<OrderResponseDto>.NotFound("Order not found.");
        }
    }
}