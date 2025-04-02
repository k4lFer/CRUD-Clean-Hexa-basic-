using Application.DTOs.Common;
using Shared.Message;

namespace Presentation.Generic
{
    public abstract class SoGeneric<T, Tot>
    {
        public Message message { get; set; } = new();
        public ContainerDto<T, Tot> Body { get; set; } = new();
    }
}