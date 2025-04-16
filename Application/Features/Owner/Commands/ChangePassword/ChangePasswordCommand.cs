using Application.DTOs.Common;
using MediatR;
using Shared.Message;

namespace Application.Features.Owner.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Result<object>>
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }

        public ChangePasswordCommand(Guid id, string password, string newPassword)
        {
            Id = id;
            Password = password;
            NewPassword = newPassword;
        }
    }
    
}