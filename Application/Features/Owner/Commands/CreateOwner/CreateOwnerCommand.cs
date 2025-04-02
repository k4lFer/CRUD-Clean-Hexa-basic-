using Application.DTOs.Owner;
using MediatR;
using Shared.Message;

namespace Application.Features.Owner.Commands.CreateOwner
{
    public class CreateOwnerCommand : IRequest<Message>
    {
        public OwnerCreateDto Owner { get; set; }
        public CreateOwnerCommand(OwnerCreateDto owner)
        {
            Owner = owner;            
        }
    }
}