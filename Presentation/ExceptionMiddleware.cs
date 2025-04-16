using System.Text.Json;
using Application.DTOs.Common;

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
            var result = Result<object>.Exception($"Ocurrió un error inesperado: {exception.Message}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)result.HttpStatusCode;

            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            return context.Response.WriteAsync(json);
        }
    }
}
