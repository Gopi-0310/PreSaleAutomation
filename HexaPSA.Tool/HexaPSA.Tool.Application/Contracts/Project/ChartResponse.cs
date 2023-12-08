using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Contracts.Project
{
    public class ChartResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string Hours { get; set; }

    }
    public class TotalHoursResponse
    {
        public double Hours { get; set; }
    }
}
