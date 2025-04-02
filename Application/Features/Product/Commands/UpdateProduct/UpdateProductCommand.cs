using Application.DTOs.Product;
using MediatR;
using Shared.Message;

namespace Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Message>
    {
        public ProductUpdateDto Product { get; set; }
        public UpdateProductCommand(ProductUpdateDto product) => Product = product;
    }
}