using Application.DTOs.Common;
using Application.DTOs.Owner;
using MediatR;
using Shared.Message;

namespace Application.Features.Owner.Queries.GetAllPag
{
    public class GetAllOwnerPagQuery : IRequest<(Message, PagedResponse<OwnerResponseDto>)>
    {
        public int pageNumber { get; set; } 
        public int pageSize { get; set; }
        public string search { get; set; }

        public GetAllOwnerPagQuery(int? pageNumber, int? pageSize, string? search) 
        { 
            this.pageNumber = pageNumber ?? 1; 
            this.pageSize = pageSize ?? 10; 
            this.search = search ?? string.Empty; 
        }
    }
}