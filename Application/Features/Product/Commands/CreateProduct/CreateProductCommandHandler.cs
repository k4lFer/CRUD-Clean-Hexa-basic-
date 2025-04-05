using Domain.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared.Message;

namespace Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Message>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
        => (_unitOfWork, _productRepository) = (unitOfWork, productRepository);
        public async Task<Message> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var message = new Message();
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                TProduct product = TProduct.Create(
                    request.Product.name,
                    request.Product.description, 
                    request.Product.stock, 
                    request.Product.price
                    );
               
                await _productRepository.AddAsync(product, cancellationToken); // 🔹 Solo agrega, sin guardar
                await _unitOfWork.CommitAsync(cancellationToken); // 🔹 Guarda los cambios en la BD

                message.Created();
                message.AddMessage("Producto creado correctamente.");
                return message;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                message.Error();
                message.AddMessage($"Error al crear el producto: {ex.Message}");
                return message;
            }
        }
    }
}