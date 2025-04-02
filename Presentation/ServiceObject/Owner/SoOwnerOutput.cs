using Application.DTOs.Common;
using Application.DTOs.Owner;
using Presentation.Generic;

namespace Presentation.ServiceObject.Owner
{
    public class SoOwnerOutput : SoGeneric<OwnerData, PagedResponse<OwnerResponseDto>>
    {
        public SoOwnerOutput() 
        { 
            
        }
    }
        public class OwnerData
        {
            public OwnerResponseDto ResponseDto { get; set; }
            public OwnerProfile Profile { get; set; }
        }
}