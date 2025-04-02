using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Message;

namespace Presentation.Generic
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GenericController<So> : ControllerBase
           where So : new()
    {
        protected readonly So _so = new();

        protected Message ValidatePartDto(object dto, List<string> listField)
        {
            var message = new Message();
            var errors = new List<string>();

            ModelState.Clear();
            TryValidateModel(dto);

            foreach (var field in listField)
            {
                if (ModelState.TryGetValue(field, out var modelState) && modelState?.Errors.Count > 0)
                {
                    foreach (var error in modelState.Errors)
                    {
                        var errorMessage = error.ErrorMessage.Contains("required") ? "El campo es obligatorio." : error.ErrorMessage;
                        if (!string.IsNullOrWhiteSpace(errorMessage))
                        {
                            errors.Add($"'{field}': {errorMessage}");
                        }
                    }
                }
            }

            if (errors.Count > 0)
            {
                message.ListMessage = errors;
                message.Error();
            }

            return message;
        }

        protected ActionResult<So> HandleException(Exception ex, Message message)
        {
            string errorMessage = ex.Message;
            if (ex.InnerException != null) 
                errorMessage += " -> " + ex.InnerException.Message;
            
            message.ListMessage.Add(errorMessage);
            message.Exception();
            return StatusCode((int)message.ToStatusCode(), _so);
        }

    }

}