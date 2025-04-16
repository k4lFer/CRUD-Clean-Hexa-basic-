using Application.DTOs.Common;
using Application.DTOs.Product;
using MediatR;
using Shared.Message;

namespace Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Result<object>>
    {
        public ProductUpdateDto Product { get; set; }
        public UpdateProductCommand(ProductUpdateDto product) => Product = product;
    }
}