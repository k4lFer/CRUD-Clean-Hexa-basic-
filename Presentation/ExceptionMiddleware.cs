using System.Text.Json;
using Shared.Message;

namespace Presentation
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Procesar la petición
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado"); // Registrar el error
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var message = new Message();
            message.Exception();
            message.AddMessage("Ocurrió un error inesperado en el servidor.");

            // Agregar detalles del error en modo desarrollo
            message.AddMessage(exception.Message);

            var response = new
            {
                message.Type,
                Errors = message.ListMessage
            };

            var result = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)message.ToStatusCode(); // Corrección aquí
            
            return context.Response.WriteAsync(result);
        }
    }
}
