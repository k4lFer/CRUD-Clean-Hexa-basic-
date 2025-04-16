using Application.Common.Interfaces;
using Application.DTOs.Common;
using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Message;

namespace Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<object>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) 
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Result<object>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            //await _unitOfWork.BeginTransactionAsync(cancellationToken);
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
                    return Result<object>.Warning("No se realizaron cambios en el producto"); // 🔹 Retorna el resultado
                }
                    await _productRepository.UpdateAsync(existingProduct, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return Result<object>.Success("Producto actualizado exitosamente"); // 🔹 Retorna el resultado
            }
            return Result<object>.NotFound("Product no encontrado"); // 🔹 Retorna el resultado
            
        }
    }
}