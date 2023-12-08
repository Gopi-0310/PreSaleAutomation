using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Contracts.Project
{
    public class TeamConfigurationResponse
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? RoleId { get; set; }
        public UserTeamResponse? user { get; set; }
        public ProjectTeamResponse? projects { get; set; }
        public RoleTeamResponse? role { get; set; }
    }
    public class UserTeamResponse
    {
        public Guid? Id { get; set; }
        public string? FullName { get; set; }
    }
    public class RoleTeamResponse
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
    }
    public class ProjectTeamResponse
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
    }


    public class TeamConfigResponse {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? RoleId { get; set; }
    }
}
