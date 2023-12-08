using AutoMapper.Configuration.Annotations;
using HexaPSA.Tool.Application.Contracts.Project;

namespace HexaPSA.Tool.Application.Contracts
{
    //public sealed record PresalesTimeTrackerResponse(Guid Id, Guid? UserId, string Activity, string Description, double? Hours, Guid? ProjectId, DateTime? ActivityDate)
    //{

    //}
    public class PresalesTimeTrackerResponse
    {
        public Guid Id { get; set; }
        public UserResponsep? user { get; set; }
        public string Activity { get; set; }
        public string Description { get; set; }
        public double? Hours { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectResponsepst? project { get; set; }
        public DateTime? ActivityDate { get; set; }
    }

    public class UserResponsep
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
    }

    public class ProjectResponsepst
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
