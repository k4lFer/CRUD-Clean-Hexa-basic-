using Application.DTOs.Product;
using MediatR;
using Shared.Message;

namespace Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Message>
    {
        public ProductCreateDto Product { get; set; }
        public CreateProductCommand(ProductCreateDto product) => Product = product;
    }
}