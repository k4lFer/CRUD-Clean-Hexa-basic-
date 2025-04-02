using Application.DTOs.Common;
using Application.DTOs.Order;
using Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Shared.Message;

namespace Application.Features.Order.Queries.GetAllPag
{
    public class GetAllOrderPagQueryHandler : IRequestHandler<GetAllOrderPagQuery, (Message, PagedResponse<OrderResponseDto>)>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetAllOrderPagQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<(Message, PagedResponse<OrderResponseDto>)> Handle(GetAllOrderPagQuery request, CancellationToken cancellationToken)
        {
            var message = new Message();
            var pagedOrders = await _orderRepository.GetAllPaged(
                request.pageNumber, 
                request.pageSize, 
                cancellationToken);

            if (pagedOrders == null)
            {
                message.NotFound();
                message.AddMessage("Orders not found.");
                return (message, null);
            }
            var orders = _mapper.Map<IEnumerable<OrderResponseDto>>(pagedOrders.Items);

            var response = PagedResponse<OrderResponseDto>.Ok(
                new PaginationResponseDto<OrderResponseDto>(orders, pagedOrders.TotalCount, request.pageNumber, request.pageSize)
            );

            message.Success();
            return (message, response);
        }   
    }

}