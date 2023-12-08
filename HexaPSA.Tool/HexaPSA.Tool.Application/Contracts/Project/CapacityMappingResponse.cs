using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Contracts.Project
{
   // public sealed record CapacityMappingResponse(Guid Id ,CapacityResourceRateResponse resource ,double Hours) { }

    public class CapacityMappingResponse
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public CapacityRoleResponse? Role { get; set; }
        public double Hours { get; set; }
        public string Location { get; set; }
        public CapacityResourceRateResponse? ResourceRate { get; set; }

        


        // Other properties


    }


}
