using Application.DTOs.Customer;
using Presentation.Generic;

namespace Presentation.ServiceObject.Customer
{
    public class SoCustomerInput : SoGenericInput<CustomerInput>
    {
        public SoCustomerInput() { }
    }
    public class CustomerInput
    {
        public CustomerCreateDto? CreateDto { get; set; }
        public CustomerUpdateDto? UpdateDto { get; set; }
    }
}