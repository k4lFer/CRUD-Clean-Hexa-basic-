using Application.DTOs.Common;
using Application.DTOs.Owner;
using MediatR;
using Shared.Message;

namespace Application.Features.Owner.Queries.GetById
{
    public class GetOwnerByIdQuery : IRequest<Result<OwnerResponseDto>>
    {
        public Guid Id { get; set; }
        public GetOwnerByIdQuery(Guid id) => Id = id;
    }
}