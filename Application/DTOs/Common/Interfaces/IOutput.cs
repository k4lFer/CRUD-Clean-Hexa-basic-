namespace Application.DTOs.Common.Interfaces
{
    public interface IOutput<T> : IHttpResponse
    {
        T? Data { get; }
        bool IsSuccess { get; }
    }
}