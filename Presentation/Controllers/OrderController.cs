using Application.Features.Order.Commands.CreateOrder;
using Application.Features.Order.Queries.CancelOrder;
using Application.Features.Order.Queries.GetAllPag;
using Application.Features.Order.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Generic;
using Presentation.ServiceObject.Order;
using Shared.Message;

namespace Presentation.Controllers
{
    public class OrderController(IMediator mediator) : GenericController<SoOrderOutput>
    {
        private readonly IMediator _mediator = mediator;
        
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<SoOrderOutput>> Create([FromBody] SoOrderInput soOrder) 
        {
            try
            {
                _so.message = ValidatePartDto(soOrder.InputDto,[
                    nameof(soOrder.InputDto.customerId),
                    nameof(soOrder.InputDto.orderDetails)
                    ]);
                _so.message = await _mediator.Send(new CreateOrderCommand(soOrder.InputDto));
                return StatusCode((int)_so.message.ToStatusCode(), _so.message);
            }
            catch (Exception ex)
            {
                return HandleException(ex, _so.message);
            } 
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<SoOrderOutput>> CancelOrder([FromQuery] Guid orderId) 
        {
            try
            {
                var command = new CancelOrderQuery(orderId);
                _so.message = await _mediator.Send(command);
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
        public async Task<ActionResult<SoOrderOutput>> GetOrder([FromQuery] Guid orderId)
        {
            try
            {
                (_so.message, _so.Body.Dto) = await _mediator.Send(new GetOrderByIdQuery(orderId));
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
        public async Task<ActionResult<SoOrderOutput>> GetAllOrders(
            [FromQuery] int? pageNumber,    
            [FromQuery] int? pageSize)
            {
                try
                {
                    (_so.message, _so.Body.Other) = await _mediator.Send(new GetAllOrderPagQuery(pageNumber, pageSize));
                    return StatusCode((int)_so.message.ToStatusCode(), _so);
                }
                catch (Exception ex)
                {
                    return HandleException(ex, _so.message);
                }
            }

    }
}