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
        public async Task<ActionResult<SoCustomerOutput>> GetById([FromQuery] Guid id)
        {
            try
            {
                (_so.message, _so.Body.Dto) = await _mediator.Send(new GetCustomerByIdQuery(id));
                return StatusCode((int)_so.message.ToStatusCode(), _so);
            }
            catch (Exception ex)
            {
                return HandleException(ex, _so.message);
            }   
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<SoCustomerOutput>> Create([FromBody] SoCustomerInput soCustomer)
        {
            try
            {
                _so.message = ValidatePartDto(soCustomer.InputDto.CreateDto,
                [
                    nameof(soCustomer.InputDto.CreateDto.firstName),
                    nameof(soCustomer.InputDto.CreateDto.lastName),
                    nameof(soCustomer.InputDto.CreateDto.email),
                    nameof(soCustomer.InputDto.CreateDto.documentNumber)
                ]);
                if (_so.message.ExistsMessage()) return StatusCode((int)_so.message.ToStatusCode(), _so);

                _so.message = await _mediator.Send(new CreateCustomerCommand(soCustomer.InputDto.CreateDto));
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
        public async Task<ActionResult<SoCustomerOutput>> Update([FromBody] SoCustomerInput soCustomer)
        {
            try
            {
                _so.message = ValidatePartDto(soCustomer.InputDto.UpdateDto,
                [
                    nameof(soCustomer.InputDto.UpdateDto.id),
                ]);
                if (_so.message.ExistsMessage()) return StatusCode((int)_so.message.ToStatusCode(), _so);

                _so.message = await _mediator.Send(new UpdateCustomerCommand(soCustomer.InputDto.UpdateDto));
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
        public async Task<ActionResult<SoCustomerOutput>> GetAllPaged(
            [FromQuery] int? pageNumber, 
            [FromQuery] int? pageSize, 
            [FromQuery] string? search)
        {
            try
            {    
                (_so.message, _so.Body.Other) = await _mediator.Send(new GetAllCustomersPagQuery(pageNumber, pageSize, search));
                return StatusCode((int)_so.message.ToStatusCode(), _so);
            }
            catch (Exception ex)
            {
                return HandleException(ex, _so.message);
            } 
        }

    }
}
