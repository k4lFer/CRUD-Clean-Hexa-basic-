using Application.DTOs.Common;
using Application.DTOs.Product;
using MediatR;
using Shared.Message;

namespace Application.Features.Product.Queries.GetAllPag
{
    public class GetAllProductsPagQuery : IRequest<(Message, PagedResponse<ProductResponseDto>)> 
    { 
        public int pageNumber { get; set; } 
        public int pageSize { get; set; }
        public string search { get; set; }

        public GetAllProductsPagQuery(int? pageNumber, int? pageSize, string? search) 
        { 
            this.pageNumber = pageNumber ?? 1; 
            this.pageSize = pageSize ?? 10; 
            this.search = search ?? string.Empty; 
        }  
    }
}