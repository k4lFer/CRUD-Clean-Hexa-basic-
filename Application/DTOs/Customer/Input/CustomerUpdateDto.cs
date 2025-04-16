namespace Application.DTOs.Customer
{
    public class CustomerUpdateDto
    {
        public Guid id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? documentType { get; set; }
        public string? documentNumber { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }

    }
}