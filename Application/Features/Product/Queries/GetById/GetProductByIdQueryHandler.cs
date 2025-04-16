using Application.DTOs.Common;
using Application.DTOs.Product;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Message;

namespace Application.Features.Product.Queries.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductResponseDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<Result<ProductResponseDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product != null)
            {
                var productDto = _mapper.Map<ProductResponseDto>(product);
                return Result<ProductResponseDto>.Success(productDto); 
            }
            return Result<ProductResponseDto>.NotFound("Product not found."); // 🔹 Retorna el resultado
        }
    }
}