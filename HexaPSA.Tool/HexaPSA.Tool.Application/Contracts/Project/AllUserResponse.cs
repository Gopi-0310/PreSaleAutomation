using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Contracts.Project
{
    public class AllUserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public RoleResponse Role { get; set; }
        public string EMail { get; set; }
    }
}
