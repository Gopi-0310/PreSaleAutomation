using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Contracts.Project
{
    public class ProjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ProjectTypeResponse> ProjectTypes { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public List<UserRoleResponse> EstimatedUsers { get; set; }
        public List<TechnologyResponse> Technology { get; set; }

        public List<WorkStreamActivityMappingResponse> WorkStreamActivities { get; set; }
        public CostSummary CostSummary { get; set; }
        public List<WorkStreamResp> WorkStreamResponses { get; set; }
        public List<WorkStreamActivityResp> WorkStreamActivityResponses { get; set; }
        public List<ResourceRateResponseExport> ResourceRateList { get; set; }
    }

    public class CostSummary
    {
       
        public List<CostByResourceResp> CostByResourceList { get; set; }
        public List<CostByWorkStream> CostByWorkStreamList { get; set; }
        public List<int> Weeks { get; set; } 

    }
    public class CostByWorkStream
    {
        public string Thread { get; set; }
        public decimal Hours { get; set; }
        public decimal Fees { get; set; }
    }
    public class CostByResourceResp
    {
        public string Code { get; set; }
        public string Role { get; set; }
        public string Location { get; set; }
        public decimal Hours { get; set; }
        public decimal Days { get; set; }
        public decimal Rate { get; set; }
        public decimal Fees { get; set; }
    }
    public class WorkStreamResp { 

        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<WorkStreamActivityResp> WorkStreamActivityList { get; set; }

    }
    public class WorkStreamActivityResp
    {
        public Guid Id { get; set; }
        public string Activity { get; set; }
        public string Role { get; set; }
        public string RoleDescription { get; set; }

        public Guid WorkStreamActivityId { get; set; }
        public double Hours { get; set; }

        public double Rate { get; set; }
        public int Week { get; set; }
        //public string? Description { get; set; }
        public Guid? ParentId { get; set; } = Guid.Empty;
        public string ParentActivity { get; set; }
        public Guid ParentActivityId { get; set; }

        public int Depth { get; set; }
        public string Name { get; set; }
        public List<WorkStreamActivityResp> WorkStreamActivities { get; set; }  


    }

    public class UserRoleResponse
    {
        public UserResponse User { get; set; }
        public RoleProjectResponse Role { get; set; }
    }
    public class TechnologyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ResourceRateResponseExport
    {
        public string Code { get; set; }
        public string Role { get; set; }
        public string Location { get; set; }
        public decimal Rate { get; set; }

    }
}
