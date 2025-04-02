using Shared.Message;

namespace Presentation.Generic
{
    public abstract class SoGenericInput<TCreate, TUpdate>
    {
        public TCreate? CreateDto { get; set; }
        public TUpdate? UpdateDto { get; set; }
    }

}