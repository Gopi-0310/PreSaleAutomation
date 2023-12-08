using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Contracts.Project
{
    public class CapacityResourceRateResponse
    {
       public Guid Id { get; set; }
       public Guid ProjectId { get; set; }
       public Guid RoleId { get; set; }
       public decimal Rate { get; set; }
    }
}
