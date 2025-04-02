using Application.DTOs.Customer;
using Presentation.Generic;

namespace Presentation.ServiceObject.Customer
{
    public class SoCustomerInput : SoGenericInput<CustomerCreateDto, CustomerUpdateDto>
    {
        public SoCustomerInput() { }
    }
}