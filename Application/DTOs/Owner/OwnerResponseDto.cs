using Domain.Enum;

namespace Application.DTOs.Owner
{
    public class OwnerResponseDto
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dni { get; set; }
        public string? ruc { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public RoleEnum role { get; set; }
        public bool status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string? createdBy { get; set; }
        public string? updatedBy { get; set; }
    }
}