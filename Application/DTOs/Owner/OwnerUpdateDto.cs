namespace Application.DTOs.Owner
{
    public class OwnerUpdateDto 
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string? Ruc { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}