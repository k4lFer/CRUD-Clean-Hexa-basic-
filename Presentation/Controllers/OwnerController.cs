using Application.Features.Owner.Commands.ChangePassword;
using Application.Features.Owner.Commands.CreateOwner;
using Application.Features.Owner.Queries.GetById;
using Application.Features.Owner.Queries.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Generic;
using Presentation.ServiceObject.Owner;
using Shared.Message;

namespace Presentation.Controllers
{
    public class OwnerController(IMediator mediator) : GenericController<SoOwnerOutput>
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetProfile()
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new GetOwnerQuery()));   
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetOwner([FromQuery] Guid id)
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new GetOwnerByIdQuery(id)));
        }   

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] SoOwnerInput soOwner) 
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new CreateOwnerCommand(soOwner.InputDto)));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ChangePassword()
        {
            return Ok();
        }

    }
}