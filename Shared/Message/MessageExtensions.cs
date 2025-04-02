using System.Net;

namespace Shared.Message
{
    public static class MessageExtensions
    {
        public static HttpStatusCode ToStatusCode(this Message message)
        {
            return message.Type switch
            {
                "error" => HttpStatusCode.BadRequest,
                "warning" => HttpStatusCode.NotFound,
                "not-found" => HttpStatusCode.NotFound,
                "created" => HttpStatusCode.Created,
                "unauthorized" => HttpStatusCode.Unauthorized,
                "timeout" => HttpStatusCode.RequestTimeout,
                "forbidden" => HttpStatusCode.Forbidden,
                "conflict" => HttpStatusCode.Conflict,
                "exception" => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.OK
            };
        }
    }
    
}