using System.Net;
using Shared.Message;

namespace Application.Common.Exceptions
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public Message ResponseMessage { get; }
        public BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            : base(message)
        {
            StatusCode = statusCode;
            ResponseMessage = new Message
            {
                Type = "error",
                ListMessage = [message]
            };
        }
    }
}