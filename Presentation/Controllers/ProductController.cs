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
        public async Task<ActionResult<SoProductOutput>> Create([FromBody] SoProductInput soProduct)
        {
            try
            {
                _so.message = await _mediator.Send(new CreateProductCommand(soProduct.CreateDto));
                return StatusCode((int)_so.message.ToStatusCode(), _so.message);
            }
            catch (Exception ex)
            {
                return HandleException(ex, _so.message);
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult<SoProductOutput>> Update([FromBody] SoProductInput soProduct)
        {
            try
            {
                _so.message = await _mediator.Send(new UpdateProductCommand(soProduct.UpdateDto));
                return StatusCode((int)_so.message.ToStatusCode(), _so.message);
            }
            catch (Exception ex)
            {
                return HandleException(ex, _so.message);
            }
        }

        [AllowAnonymous]    
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<SoProductOutput>> GetAllPaged
        (
            [FromQuery] int? pageNumber, 
            [FromQuery] int? pageSize, 
            [FromQuery] string? search
        )
        {
            try
            {
                (_so.message, _so.Body.Other) = await _mediator.Send(new GetAllProductsPagQuery(pageNumber, pageSize, search));
                return StatusCode((int)_so.message.ToStatusCode(), _so);
            }
            catch (Exception ex)
            {
                return HandleException(ex, _so.message);
            } 

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<SoProductOutput>> GetById([FromQuery] Guid id)
        {
            try 
            {
                (_so.message, _so.Body.Dto) = await _mediator.Send(new GetProductByIdQuery(id));
                return StatusCode((int)_so.message.ToStatusCode(), _so);
            }
            catch (Exception ex)
            {
                return HandleException(ex, _so.message);
            }

        }
    }
}