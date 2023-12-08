
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Contracts.Project
{
    public class WorkStreamActivityResponse
    {
        public Guid Id { get; set; }
        public string Activity { get; set; }
        public Guid RoleId { get; set; }
        public Guid WorkStreamActivityId { get; set; }
        public double Hours { get; set; }
        public double Week { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; } = Guid.Empty;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }


   
    public class WorkStreamActivityMappingResponse
    {
        public Guid Id { get; set; }
        public string Activity { get; set; }
        public RoleResponse? Role { get; set; }
        public WorkStreamMappingResponse? WorkStream { get; set; }
        public double Hours { get; set; }
        public double Week { get; set; }
        public string? Description { get; set; }

        public ResourceRateResponseMapping? Rate { get; set; }
        public Guid? ParentId { get; set; } = null;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }


    public class WorkStreamChartsMappingResponse
    {
        public double Hours { get; set; }
        public double Week { get; set; }
        public Guid WorkStreamActivityId { get; set; }

        public string Name { get; set; }
        public Guid? ProjectId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }

    public class WorkStreamChartsResponse
    {
        public Guid ProjectId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

       
    }

    public class WorkStreamActivityWeeksResponse
    {
        public Guid Id { get; set; }
        public Guid WorkStreamActivityId { get; set; }
        public double Hours { get; set; }
        public double Week { get; set; }
        public WorkStreamProjectResponse? Project { get; set; }

    }

    public class WorkStreamProjectResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
    }


    public class WorkStreamChartDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
