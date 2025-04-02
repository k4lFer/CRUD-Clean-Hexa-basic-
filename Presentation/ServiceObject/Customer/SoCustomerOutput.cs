using Application.DTOs.Common;
using Application.DTOs.Customer;
using Presentation.Generic;

namespace Presentation.ServiceObject.Customer
{
    public class SoCustomerOutput : SoGeneric<CustomerResponseDto, PagedResponse<CustomerResponseDto>>
    {
        public SoCustomerOutput() { }
    }

}