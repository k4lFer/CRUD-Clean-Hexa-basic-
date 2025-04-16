using Application.DTOs.Common;
using Application.DTOs.Owner;
using MediatR;

namespace Application.Features.Owner.Commands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<Result<object>>
    {
        public OwnerUpdateDto Owner { get; set; }
        public UpdateProfileCommand(OwnerUpdateDto owner)
        {
            Owner = owner;
        }
    }
}