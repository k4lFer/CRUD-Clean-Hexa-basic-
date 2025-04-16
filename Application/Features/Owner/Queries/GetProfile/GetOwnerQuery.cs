using Application.DTOs.Common;
using Application.DTOs.Owner;
using MediatR;

namespace Application.Features.Owner.Queries.GetProfile
{
    public class GetOwnerQuery : IRequest<Result<OwnerResponseDto>>
    {
        
    }
}