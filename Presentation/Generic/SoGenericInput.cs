using Shared.Message;

namespace Presentation.Generic
{
    public abstract class SoGenericInput<TInput>
    {
        public TInput? InputDto { get; set; }
    }

}