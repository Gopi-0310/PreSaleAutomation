using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Contracts.Technology
{
    public sealed record TechnologyDropResponse(Guid Id, string Name)
    {
    }
}
