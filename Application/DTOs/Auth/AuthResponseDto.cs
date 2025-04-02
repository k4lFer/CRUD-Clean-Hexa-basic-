using Application.DTOs.Common;
using Domain.Enum;

namespace Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public Guid id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public RoleEnum role { get; set; }
        
        public Tokens? tokens { get; set; }
    }
}