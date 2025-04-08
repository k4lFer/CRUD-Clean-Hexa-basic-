using Domain.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Shared.Message;
using Application.Common.Interfaces;

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
            TProduct product = TProduct.Create(
                request.Product.name,
                request.Product.description, 
                request.Product.stock, 
                request.Product.price
                );
               
            await _productRepository.AddAsync(product, cancellationToken); // 🔹 Solo agrega, sin guardar
            await _unitOfWork.SaveChangesAsync(cancellationToken); // 🔹 Guarda los cambios en la BD

            message.Created();
            message.AddMessage("Producto creado correctamente.");
            return message;
        }
    }
}