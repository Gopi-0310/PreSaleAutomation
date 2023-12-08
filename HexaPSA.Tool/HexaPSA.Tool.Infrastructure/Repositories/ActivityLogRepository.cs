using Dapper;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using System.Data;
using System.Data.Common;

namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly DapperDataContext _context;
        public ActivityLogRepository(DapperDataContext context)
        {
            _context = context;
        }
        public async Task<ActivityLog> AddAsync(ActivityLog activityLog)
        {
            var query = "INSERT INTO ActivityLog (ProjectId,LogActivity,CreatedDate) OUTPUT inserted.Id VALUES (@ProjectId,@LogActivity,@CreatedDate)";

            var parameters = new DynamicParameters();
            parameters.Add("ProjectId", activityLog.ProjectId, DbType.Guid);
            parameters.Add("LogActivity", activityLog.LogActivity, DbType.String);
            // parameters.Add("CreatedBy", activityLog.CreatedBy, DbType.Guid);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, parameters);
                var createdActivityLog = new ActivityLog
                {
                    Id = id,
                    ProjectId = activityLog.ProjectId,
                    LogActivity = activityLog.LogActivity,
                    CreatedDate = activityLog.CreatedDate,

                };
                return createdActivityLog;
            }

        }

        public Task<ActivityLog> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ActivityLog>> GetAllAsync()
        {
            var query = "SELECT * FROM ActivityLog";
            using (var connection = _context.CreateConnection())
            {
                var items = await connection.QueryAsync<ActivityLog>(query);
                return items;

            }
        }

       

        public async Task<IEnumerable<RecentExportAcivitiesResponse>> GetExportRecentAcivities()
        {
            var query = @"
                        SELECT TOP (5) 
                        p.Name AS ProjectName,
                        al.LogActivity,
                        al.CreatedDate
                        FROM dbo.ActivityLog al
                        JOIN dbo.Project p ON p.Id = al.ProjectId
                        ORDER BY al.CreatedDate DESC";

            using (var connection = _context.CreateConnection())
            {
                var items = await connection.QueryAsync<RecentExportAcivitiesResponse>(query);
                return items;
            }
        }

        public async Task<IEnumerable<RecentAcivitiesResponse>> GetRecentAcivities()
        {

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<RecentAcivitiesResponse>("GetRecentActivities", commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public Task<ActivityLog> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityLog> UpdateAsync(ActivityLog item)
        {
            throw new NotImplementedException();
        }
    }
}
