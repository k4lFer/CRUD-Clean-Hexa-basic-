using Domain.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Shared.Message;
using Application.Common.Interfaces;
using Application.Interfaces.ExternalServices;
using Application.DTOs.Common;

namespace Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, ICloudinaryService cloudinaryService)
        => (_unitOfWork, _productRepository, _cloudinaryService) = (unitOfWork, productRepository, cloudinaryService);
        public async Task<Result<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            TProduct product = TProduct.Create(
                request.Product.name,
                request.Product.description, 
                request.Product.stock, 
                request.Product.price
                );
               
            await _productRepository.AddAsync(product, cancellationToken); // 🔹 Solo agrega, sin guardar
            await _unitOfWork.SaveChangesAsync(cancellationToken); // 🔹 Guarda los cambios en la BD

            return Result<object>.Created("Product created successfully"); // 🔹 Retorna el resultado
        }
    }
}