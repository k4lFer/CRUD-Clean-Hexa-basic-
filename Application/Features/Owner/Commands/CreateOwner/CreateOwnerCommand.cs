using Application.DTOs.Common;
using Application.DTOs.Owner;
using MediatR;
using Shared.Message;

namespace Application.Features.Owner.Commands.CreateOwner
{
    public class CreateOwnerCommand : IRequest<Result<object>>
    {
        public OwnerCreateDto Owner { get; set; }
        public CreateOwnerCommand(OwnerCreateDto owner)
        {
            Owner = owner;            
        }
    }
}