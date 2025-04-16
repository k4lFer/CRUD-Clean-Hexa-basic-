using Application.DTOs.Common;
using Application.DTOs.Order;
using Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Order.Queries.GetAllPag
{
    public class GetAllOrderPagQueryHandler : IRequestHandler<GetAllOrderPagQuery, Result<PagedResponse<OrderResponseDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetAllOrderPagQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedResponse<OrderResponseDto>>> Handle(GetAllOrderPagQuery request, CancellationToken cancellationToken)
        {
            var pagedOrders = await _orderRepository.GetAllPaged(
                request.pageNumber, 
                request.pageSize, 
                cancellationToken);

            if (pagedOrders != null)
            {
                var orders = _mapper.Map<IEnumerable<OrderResponseDto>>(pagedOrders.Items);
                var response = PagedResponse<OrderResponseDto>.Ok(
                    new PaginationResponseDto<OrderResponseDto>(orders, pagedOrders.TotalCount, request.pageNumber, request.pageSize)
                );
                
                return Result<PagedResponse<OrderResponseDto>>.Success(response);
            }

            return Result<PagedResponse<OrderResponseDto>>.NotFound("No orders found.");            

        }   
    }

}