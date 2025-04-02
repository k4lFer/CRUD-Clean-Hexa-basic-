using Application.DTOs.Common;
using Application.DTOs.Product;
using Presentation.Generic;

namespace Presentation.ServiceObject.Product
{
    public class SoProductOutput : SoGeneric<ProductResponseDto, PagedResponse<ProductResponseDto>>
    {
        public SoProductOutput() { }
    }
}