using Application.Features.Authentication.Commands.RefreshToken;
using Application.Features.Authentication.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Generic;
using Presentation.ServiceObject.Auth;

namespace Presentation.Controllers
{
    public class AuthController(IMediator mediador) : GenericController<SoAuthOutput> 
    { 
        private readonly IMediator _mediator = mediador;

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Auth([FromBody] SoAuthInput soAuth) 
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new AuthQuery(soAuth.InputDto)));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RefreshToken([FromForm] string token) 
        {
            return ResponseHelper.GetActionResult(await _mediator.Send(new RefreshTokenCommand(token)));
        }    
    }
}