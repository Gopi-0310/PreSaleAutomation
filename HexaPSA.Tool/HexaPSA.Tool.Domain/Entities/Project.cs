using System;

namespace HexaPSA.Tool.Domain.Entities
{
    public class Project : EntityBase<Guid>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectTypeId { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }

        public virtual ICollection<CapacityUtilization> CapacityUtilizations { get; set; } = new List<CapacityUtilization>();

        public virtual ICollection<PresalesTimeTracker> PresalesTimeTrackers { get; set; } = new List<PresalesTimeTracker>();

        public virtual ProjectType? ProjectType { get; set; }

        public virtual ICollection<ProjectUserEstimationMapping> ProjectUserEstimationMappings { get; set; } = new List<ProjectUserEstimationMapping>();

        public virtual ICollection<ResourceRate> ResourceRates { get; set; } = new List<ResourceRate>();

        public virtual ICollection<TeamConfiguration> TeamConfigurations { get; set; } = new List<TeamConfiguration>();
        public virtual ICollection<WorkStream> WorkStreams { get; set; } = new List<WorkStream>();

        public virtual ICollection<Technology> Technologies { get;}


        public virtual ICollection<ProjectTechMapping> ProjectTechMappings { get; set; }

    }
}
