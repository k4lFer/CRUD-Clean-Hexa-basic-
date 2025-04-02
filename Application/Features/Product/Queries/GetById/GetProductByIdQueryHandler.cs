using Application.DTOs.Product;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Message;

namespace Application.Features.Product.Queries.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, (Message, ProductResponseDto)>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<(Message, ProductResponseDto)> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var message = new Message();
            var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product == null)
            {
                message.NotFound();
                message.AddMessage("Product not found.");
                return (message, null);
            }
            var productDto = _mapper.Map<ProductResponseDto>(product);
            message.Success();
            return (message, productDto);
        }
    }
}