using Application.DTOs.Common;
using Application.Features.Customer.Commands.CreateCustomer;
using Application.Features.Customer.Commands.UpdateCustomer;
using Application.Features.Customer.Queries.GetAll;
using Application.Features.Customer.Queries.GetAllPag;
using Application.Features.Customer.Queries.GetById;
using Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Generic;
using Presentation.ServiceObject;
using Presentation.ServiceObject.Customer;
using Shared.Message;

namespace Presentation.Controllers
{
    public class CustomerController(IMediator mediator) : GenericController<SoCustomerOutput>
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new GetCustomerByIdQuery(id)));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] SoCustomerInput input)
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new CreateCustomerCommand(input.InputDto.CreateDto)));           
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] SoCustomerInput soCustomer)
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new UpdateCustomerCommand(soCustomer.InputDto.UpdateDto)));
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllPaged(
            [FromQuery] int? pageNumber, 
            [FromQuery] int? pageSize, 
            [FromQuery] string? search)
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new GetAllCustomersPagQuery(pageNumber, pageSize, search)));
        }

    }
}
