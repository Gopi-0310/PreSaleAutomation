namespace HexaPSA.Tool.Application.Contracts.Project
{
    public class RecentAcivitiesResponse
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Activity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
