using Application.DTOs.Product;
using Presentation.Generic;

namespace Presentation.ServiceObject.Product
{
    public class SoProductInput : SoGenericInput<ProductInput> 
    { 
        public SoProductInput() { }
    }
    public class ProductInput
    {
        public ProductCreateDto? CreateDto { get; set; }
        public ProductUpdateDto? UpdateDto { get; set; }
    }
}