using Application.DTOs.Common;
using Application.DTOs.Order;
using Presentation.Generic;

namespace Presentation.ServiceObject.Order
{
    public class SoOrderOutput : SoGeneric<OrderResponseDto, PagedResponse<OrderResponseDto> >
    {
        public SoOrderOutput() { }
    }
}