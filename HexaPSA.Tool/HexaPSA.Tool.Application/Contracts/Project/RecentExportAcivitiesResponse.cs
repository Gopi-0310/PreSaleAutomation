using System;

namespace HexaPSA.Tool.Application.Contracts.Project
{
    public class RecentExportAcivitiesResponse
    {
        public string ProjectName { get; set; }
        public string LogActivity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
