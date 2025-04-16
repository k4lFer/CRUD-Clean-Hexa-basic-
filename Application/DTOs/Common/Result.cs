using System.Net;
using Application.DTOs.Auth;
using Application.DTOs.Common.Interfaces;

namespace Application.DTOs.Common
{
    public class Result<T> : IOutput<T>
    {
        // Propiedades de estado
        public bool IsSuccess { get; private set; }

        // Datos del resultado (si aplica)
        public T? Data { get; private set; }

        // Mensajes de respuesta
        public IEnumerable<MessageDto> Messages { get; private set; }

        // Código de estado HTTP
        public HttpStatusCode HttpStatusCode { get; private set; }

        // Constructor privado para asegurar que la creación se haga a través de los métodos estáticos
        private Result(T? data, HttpStatusCode httpStatusCode, IEnumerable<MessageDto> messages, bool isSuccess)
        {
            Data = data;
            HttpStatusCode = httpStatusCode;
            Messages = messages;
            IsSuccess = isSuccess;
        }

        // Métodos estáticos para crear respuestas comunes sin necesidad de especificar el código de estado manualmente
        public static Result<T> Success(T data, string message = "Operation successful")
        {
            var messageDto = new MessageDto { Type = "success" };
            messageDto.AddMessage(message);
            return new Result<T>(data, HttpStatusCode.OK, [messageDto], true);
        }

        public static Result<T> Created(T data, string message = "Created successfully")
        {
            var messageDto = new MessageDto { Type = "created" };
            messageDto.AddMessage(message);
            return new Result<T>(data, HttpStatusCode.Created, [messageDto], true);
        }

        public static Result<T> Error(string message)
        {
            var messageDto = new MessageDto { Type = "error" };
            messageDto.AddMessage(message);
            return new Result<T>(default, HttpStatusCode.BadRequest, [messageDto], false); // Predeterminado a BadRequest
        }

        public static Result<T> Warning(string message)
        {
            var messageDto = new MessageDto { Type = "warning" };
            messageDto.AddMessage(message);
            return new Result<T>(default, HttpStatusCode.NotFound, [messageDto], false);
        }

        public static Result<T> NotFound(string message)
        {
            var messageDto = new MessageDto { Type = "not-found" };
            messageDto.AddMessage(message);
            return new Result<T>(default, HttpStatusCode.NotFound, [messageDto], false);
        }

        public static Result<T> Exception(string message)
        {
            var messageDto = new MessageDto { Type = "exception" };
            messageDto.AddMessage(message);
            return new Result<T>(default, HttpStatusCode.InternalServerError, [messageDto], false);
        }

        public static Result<T> Conflict(string message)
        {
            var messageDto = new MessageDto { Type = "conflict" };
            messageDto.AddMessage(message);
            return new Result<T>(default, HttpStatusCode.Conflict, [messageDto], false);
        }
        public static Result<T> Unauthorized(string message)
        {
            var messageDto = new MessageDto { Type = "unauthorized" };
            messageDto.AddMessage(message);
            return new Result<T>(default, HttpStatusCode.Unauthorized, [messageDto], false);
        }

        public static Result<T> Failure(IEnumerable<MessageDto> messages, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new Result<T>(default, httpStatusCode, messages, false);
        }
    }
}