using Application.DTOs.Product;
using Presentation.Generic;

namespace Presentation.ServiceObject.Product
{
    public class SoProductInput : SoGenericInput<ProductCreateDto, ProductUpdateDto> 
    { 
        public SoProductInput() { }
    }
}