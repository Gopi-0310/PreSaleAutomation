namespace HexaPSA.Tool.Domain.Entities
{
    public class ActivityLog : EntityBase<Guid>
    {
        public Guid? ProjectId { get; set; }
        public string LogActivity { get; set; }
    }
}
