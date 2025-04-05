using Application.DTOs.Common;
using Application.DTOs.Owner;
using AutoMapper;
using Presentation.Generic;

namespace Presentation.ServiceObject.Owner
{
    public class SoOwnerOutput : SoGeneric<OwnerResponseDto, PagedResponse<OwnerResponseDto>>
    {
        public SoOwnerOutput() 
        { 
            
        }
    }
}