using Application.DTOs.Common;
using Application.DTOs.Product;
using MediatR;
using Shared.Message;

namespace Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Result<object>>
    {
        public ProductCreateDto Product { get; set; }
        public CreateProductCommand(ProductCreateDto product) => Product = product;
    }
}