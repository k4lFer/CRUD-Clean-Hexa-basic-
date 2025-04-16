using Application.DTOs.Common;
using Application.DTOs.Product;
using MediatR;
using Shared.Message;

namespace Application.Features.Product.Queries.GetById
{
    public class GetProductByIdQuery : IRequest<Result<ProductResponseDto>>
    {
        public Guid ProductId { get; set; }

        public GetProductByIdQuery(Guid productId) => ProductId = productId;

    }
}