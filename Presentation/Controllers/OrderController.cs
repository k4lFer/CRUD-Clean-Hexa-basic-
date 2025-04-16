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
        public async Task<IActionResult> Create([FromBody] SoOrderInput soOrder) 
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new CreateOrderCommand(soOrder.InputDto)));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CancelOrder([FromQuery] Guid orderId) 
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new CancelOrderQuery(orderId)));
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetOrder([FromQuery] Guid orderId)
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new GetOrderByIdQuery(orderId)));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllOrders(
            [FromQuery] int? pageNumber,    
            [FromQuery] int? pageSize)
            {
                return ResponseHelper.GetActionResult(await _mediator.Send(new GetAllOrderPagQuery(pageNumber, pageSize)));
            }

    }
}