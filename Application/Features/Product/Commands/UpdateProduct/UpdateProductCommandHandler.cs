using Application.Common.Interfaces;
using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Message;

namespace Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Message>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) 
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Message> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var message = new Message();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            var existingProduct = await _productRepository.GetByIdAsync(request.Product.id, cancellationToken);
            if (existingProduct != null)
            {
                var originalValues = new
                {
                    existingProduct.name,
                    existingProduct.description,
                    existingProduct.stock,
                    existingProduct.price
                };
                
                existingProduct.Update(
                    request.Product.name,
                    request.Product.description,
                    request.Product.stock ?? 0,
                    request.Product.price ?? 0
                );

                if(existingProduct.name == originalValues.name &&
                    existingProduct.description == originalValues.description &&
                    existingProduct.stock == originalValues.stock &&
                    existingProduct.price == originalValues.price)
                {
                    message.AddMessage("No se realizaron cambios en el producto.");
                    message.Warning();
                    return message;
                }
                    await _productRepository.UpdateAsync(existingProduct, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    message.Success();
                    message.AddMessage("Producto actualizado exitosamente.");
                    return message;
            }
            message.NotFound();
            message.AddMessage("Product not encontrado");
            return message;
            
        }
    }
}