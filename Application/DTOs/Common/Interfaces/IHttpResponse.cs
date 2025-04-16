using System.Net;

namespace Application.DTOs.Common.Interfaces
{
    public interface IHttpResponse
    {
        HttpStatusCode HttpStatusCode { get; }
        IEnumerable<MessageDto> Messages { get; }
    }

}