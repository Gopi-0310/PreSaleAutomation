namespace HexaPSA.Tool.Domain.Entities
{
    public class ProjectTechMapping : EntityBase<Guid>
    {
        public Guid ProjectId { get; set; }
        public Guid TechnologyId { get; set; }
        public virtual Project Project { get; set; }
        public virtual Technology Technology { get; set; }
        public string CurrentStatus { get; set; }
    }
}
