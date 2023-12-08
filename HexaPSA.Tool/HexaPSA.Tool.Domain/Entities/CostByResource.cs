using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Domain.Entities
{
    
    public class CostByResource : EntityBase<Guid>
    {
        
        public Guid? RoleId { get; set; }

        public double? Hours { get; set; }

        public decimal? Rate { get; set; }
       
        public virtual Role? Role { get; set; }

        public ResourceRate? ResourceRate { get; set; }


    }

}
