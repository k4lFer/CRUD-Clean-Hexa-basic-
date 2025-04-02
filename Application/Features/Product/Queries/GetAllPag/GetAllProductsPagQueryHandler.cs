using Application.DTOs.Common;
using Application.DTOs.Product;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Message;

namespace Application.Features.Product.Queries.GetAllPag
{
    public class GetAllProductsPagQueryHandler : IRequestHandler<GetAllProductsPagQuery, (Message, PagedResponse<ProductResponseDto>)>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductsPagQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<(Message, PagedResponse<ProductResponseDto>)> Handle(GetAllProductsPagQuery request, CancellationToken cancellationToken)
        {
            var message = new Message();
            var pagedProducts = await _productRepository.GetAllPaged(
                request.pageNumber,
                request.pageSize,
                request.search,
                cancellationToken);

            if(pagedProducts == null)
            {
                message.NotFound();
                message.AddMessage("Productos no encontrados");
                return(message, null); 
            } 

            var products = _mapper.Map<IEnumerable<ProductResponseDto>>(pagedProducts.Items);

            var response = PagedResponse<ProductResponseDto>.Ok(
                new PaginationResponseDto<ProductResponseDto>(products, pagedProducts.TotalCount, request.pageNumber, request.pageSize)
            );

            message.Success();
            return (message, response);
        }
    }

}   