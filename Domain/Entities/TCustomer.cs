using Domain.Common;
using Domain.Events.TCustomer;

namespace Domain.Entities
{
    public class TCustomer : BaseEntity
    {
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public string documentType { get; private set; }
        public string documentNumber { get; private set; }
        // public string address { get; set; }
        public string email { get; private set; }
        public string phoneNumber { get; private set; }

        public DateTime createdAt { get; private set; }
        public DateTime updatedAt { get; private set; }

        private TCustomer() { }

        private TCustomer(string FirstName, string LastName, string DocumentType, string DocumentNumber, string Email, string PhoneNumber)
        {
            firstName = FirstName;
            lastName = LastName;
            documentType = DocumentType;
            documentNumber = DocumentNumber;
            email = Email;
            phoneNumber = PhoneNumber;  
            createdAt = DateTime.UtcNow;
            updatedAt = DateTime.UtcNow;
        }
        
        public static TCustomer Create(string firstName, string lastName, string documentType, 
                                    string documentNumber, string email, string phoneNumber)
        {
            var customer = new TCustomer(firstName, lastName, documentType, documentNumber, email, phoneNumber);
            customer.AddDomainEvent(new CreateCustomerEvent($"{customer.firstName} {customer.lastName}", customer.email));
            return customer;
        }


        public void Update(string firstName, string lastName, string documentType, 
                         string documentNumber, string email, string phoneNumber)
        {
            this.firstName = firstName ?? this.firstName;
            this.lastName = lastName ?? this.lastName;
            this.documentType = documentType ?? this.documentType;
            this.documentNumber = documentNumber ?? this.documentNumber;            
            this.phoneNumber = phoneNumber ?? this.phoneNumber;
            this.email = email ?? this.email;
            updatedAt = DateTime.UtcNow;
        }

    }
}
