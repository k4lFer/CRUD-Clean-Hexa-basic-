using Application.DTOs.Common;
using Application.DTOs.Order;
using MediatR;
using Shared.Message;

namespace Application.Features.Order.Queries.GetAllPag
{
    public class GetAllOrderPagQuery : IRequest<Result<PagedResponse<OrderResponseDto>>>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public GetAllOrderPagQuery(int? pageNumber, int? pageSize)
        {
            this.pageNumber = pageNumber ?? 1;
            this.pageSize = pageSize ?? 10;
        }   
    }

}