using Application.DTOs.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Generic;

namespace Presentation 
{
    public static class ResponseHelper
    {
        public static IActionResult GetActionResult(IHttpResponse output)
        {
            if (output.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return new OkObjectResult(output);

            if (output.HttpStatusCode == System.Net.HttpStatusCode.Created)
                return new OkObjectResult(output);

            if (output.HttpStatusCode == System.Net.HttpStatusCode.Forbidden)
                return new BadRequestObjectResult(output);

            if (output.HttpStatusCode == System.Net.HttpStatusCode.NoContent)
                return new NoContentResult();

            if (output.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
                return new NotFoundObjectResult(output);

            if (output.HttpStatusCode == System.Net.HttpStatusCode.BadRequest)
                return new BadRequestObjectResult(output);

            if (output.HttpStatusCode == System.Net.HttpStatusCode.Conflict)
                return new ConflictObjectResult(output);

            if (output.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
                return new UnauthorizedObjectResult(output);           

            return new BadRequestObjectResult(output);
        }
    }
}