using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Contracts.Project
{
    public class ProjectResponseAll
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ProjectTypeResponse> ProjectTypes { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
    }
}
