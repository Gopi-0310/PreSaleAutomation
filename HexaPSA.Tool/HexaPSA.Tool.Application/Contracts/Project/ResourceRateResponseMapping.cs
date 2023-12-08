using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Contracts.Project
{
   
    public sealed record ResourceRateResponseMapping
    {
        public Guid? Id { get; set; }
        public decimal? Rate { get; set; }
    }
}
