using Domain.Common;
using Domain.Enum;

namespace Domain.Entities
{
    public class TOwner : BaseEntity
    {
        public string username { get; private set; }
        public string password { get; private set; }
        public string email { get; private set; }
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public string dni { get; private set; }
        public string? ruc { get; private set; }
        public string address { get; private set; }
        public string phoneNumber { get; private set; }
        public RoleEnum role { get; private set; }
        public bool status { get; private set; }
        public Guid? createdBy { get; private set; }
        public Guid? updatedBy { get; private set; }
        public DateTime createdAt { get; private set; }
        public DateTime updatedAt { get; private set; }

        private TOwner() { }

        private TOwner(string Username, string Password, string Email, string FirstName, string LastName, string Dni, string? Ruc, string Address, string PhoneNumber, RoleEnum Role)
        {
            username = Username;
            password = Password;
            email = Email;
            firstName = FirstName;
            lastName = LastName;
            dni = Dni;
            ruc = Ruc;
            address = Address;
            phoneNumber = PhoneNumber;
            role = Role;
            status = true;
            createdAt = DateTime.UtcNow;
            updatedAt = DateTime.UtcNow;
        }

        public static TOwner Create(string username, string password, string email, string firstName, string lastName, string dni, string? ruc, string address, string phoneNumber, RoleEnum role)
        {
            return new TOwner(username, password, email, firstName, lastName, dni, ruc, address, phoneNumber, role);
        }

        // Metodo para actualizar la contraseña
        public void UpdatePassword(string newPassword)
        {
            password = newPassword;
            updatedAt = DateTime.UtcNow;
        }

        
        
    }
}