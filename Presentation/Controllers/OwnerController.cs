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
        public async Task<ActionResult<SoOwnerOutput>> GetProfile()
        {
            try
            {
                _so.Body.Dto.Profile = await _mediator.Send(new GetOwnerQuery());
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
        public async Task<ActionResult<SoOwnerOutput>> GetOwner([FromQuery] Guid id)
        {
            try
            {
                (_so.message, _so.Body.Dto.ResponseDto) = await _mediator.Send(new GetOwnerByIdQuery(id));
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
        public async Task<ActionResult<SoOwnerOutput>> Create([FromBody] SoOwnerInput soOwner) 
        {
            try
            {
                _so.message = await _mediator.Send(new CreateOwnerCommand(soOwner.CreateDto));
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
        public async Task<ActionResult<SoOwnerOutput>> ChangePassword()
        {
            try
            {
                return StatusCode((int)_so.message.ToStatusCode(), _so);
            }
            catch (Exception ex)
            {
                return HandleException(ex, _so.message);
            }
        }

    }
}