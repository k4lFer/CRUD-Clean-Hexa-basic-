namespace Application.DTOs.Common
{
    public class ContainerDto<T, TOt>
    {
        public T? Dto { get; set; }
        public IEnumerable<T>? ListDto { get; set; }
        public TOt? Other { get; set; }
    }
}