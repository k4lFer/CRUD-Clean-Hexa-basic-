using Application.Features.Authentication.Commands.RefreshToken;
using Application.Features.Authentication.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Generic;
using Presentation.ServiceObject;
using Presentation.ServiceObject.Auth;
using Shared.Message;

namespace Presentation.Controllers
{
    public class AuthController(IMediator mediador) : GenericController<SoAuthOutput> 
    { 
        private readonly IMediator _mediator = mediador;

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<SoAuthOutput>> Auth([FromBody] SoAuthInput soAuth) 
        {
            try
            {
                _so.message = ValidatePartDto(soAuth.InputDto, 
                [
                    nameof(soAuth.InputDto.username), 
                    nameof(soAuth.InputDto.password)
                ]);
                if (_so.message.ExistsMessage()) return StatusCode((int)_so.message.ToStatusCode(), _so);
                (_so.message, _so.Body.Dto) = await _mediator.Send(new AuthQuery(soAuth.InputDto));
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
        public async Task<ActionResult<SoAuthOutput>> RefreshToken([FromForm] string token) 
        {
            (_so.message, _so.Body.Dto) = await _mediator.Send(new RefreshTokenCommand(token));
            return StatusCode((int)_so.message.ToStatusCode(), _so);
        }    
    }
}