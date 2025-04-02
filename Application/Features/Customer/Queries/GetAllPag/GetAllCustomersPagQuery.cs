using Application.DTOs.Common;
using Application.DTOs.Customer;
using MediatR;
using Shared.Message;

namespace Application.Features.Customer.Queries.GetAllPag
{
    public class GetAllCustomersPagQuery :  IRequest<(Message, PagedResponse<CustomerResponseDto>)>
    {

        public int pageNumber { get; set; } 
        public int pageSize { get; set; }
        public string seach { get; set; }

        public GetAllCustomersPagQuery(int? pageNumber, int? pageSize, string? seach) 
        { 
            this.pageNumber = pageNumber ?? 1; 
            this.pageSize = pageSize ?? 10; 
            this.seach = seach ?? string.Empty; 
        }

    }
}