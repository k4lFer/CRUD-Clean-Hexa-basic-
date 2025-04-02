using Domain.Common;
using MediatR;

namespace Domain.Events.TCustomer
{
    public class CreateCustomerEvent : BaseEvent
    {
        public string fullName { get; }
        public string email { get; }

        public CreateCustomerEvent(string FullName, string Email)
        {
            fullName = FullName;
            email = Email;
        }
    }

}