using Application.Features.Product.Commands.CreateProduct;
using Application.Features.Product.Commands.UpdateProduct;
using Application.Features.Product.Queries.GetAllPag;
using Application.Features.Product.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Generic;
using Presentation.ServiceObject.Product;
using Shared.Message;

namespace Presentation.Controllers
{
    public class ProductController : GenericController<SoProductOutput>
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) 
        {
            _mediator = mediator;
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] SoProductInput soProduct)
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new CreateProductCommand(soProduct.InputDto.CreateDto)));
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] SoProductInput soProduct)
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new UpdateProductCommand(soProduct.InputDto.UpdateDto)));
        }

        [AllowAnonymous]    
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllPaged
        (
            [FromQuery] int? pageNumber, 
            [FromQuery] int? pageSize, 
            [FromQuery] string? search
        )
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new GetAllProductsPagQuery(pageNumber, pageSize, search)));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new GetProductByIdQuery(id)));
        }
    }
}