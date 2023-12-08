using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using System.Data;
using Dapper;

namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class PresalesTimeTrackerRepository : IPresalesTimeTrackerRepository
    {
        private readonly DapperDataContext _context;

        public PresalesTimeTrackerRepository(DapperDataContext context)
        {
            _context = context;
        }

        public async Task<PresalesTimeTracker> GetByNameAsync(string name)
        {
            var query = "SELECT * FROM PresalesTimeTracker WHERE Name = @name";
            using (var connection = _context.CreateConnection())
            {
                var timeTracker = await connection.QuerySingleOrDefaultAsync<PresalesTimeTracker>(query, new { name });
                return timeTracker;
            }
        }

        public async Task<IEnumerable<PresalesTimeTracker>> GetAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT pt.*, u.*, p.*
                        FROM PresalesTimeTracker pt
                        LEFT JOIN [User] u ON pt.UserId = u.Id
                        LEFT JOIN Project p ON pt.ProjectId = p.Id
                        WHERE pt.IsDestroyed = 0";
                var timeTrackers = await connection.QueryAsync<PresalesTimeTracker, User, Project, PresalesTimeTracker>(sql, (pt, user, project) => {
                    pt.User = user;
                    pt.Project = project;
                    return pt;
                }, splitOn: "Id,Id,Id");

                return timeTrackers;
            }
        }

        public async Task<PresalesTimeTracker> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM PresalesTimeTracker WHERE Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var timeTracker = await connection.QuerySingleOrDefaultAsync<PresalesTimeTracker>(query, new { id });
                return timeTracker;
            }
        }

        public async Task<PresalesTimeTracker> AddAsync(PresalesTimeTracker timeTracker)
        {
            var query = "INSERT INTO PresalesTimeTracker (UserId, Activity, Description, Hours, ProjectId, ActivityDate, CreatedDate, CreatedBy) " +
                        "OUTPUT inserted.Id " +
                        "VALUES (@UserId, @Activity, @Description, @Hours, @ProjectId, @ActivityDate, @CreatedDate, @CreatedBy)";

            var parameters = new DynamicParameters();
            parameters.Add("UserId", timeTracker.UserId, DbType.Guid);
            parameters.Add("Activity", timeTracker.Activity, DbType.String);
            parameters.Add("Description", timeTracker.Description, DbType.String);
            parameters.Add("Hours", timeTracker.Hours, DbType.Double);
            parameters.Add("ProjectId", timeTracker.ProjectId, DbType.Guid);
            parameters.Add("ActivityDate", timeTracker.ActivityDate, DbType.DateTime);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
            parameters.Add("CreatedBy", timeTracker.CreatedBy, DbType.Guid);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, parameters);
                var createdTimeTracker = new PresalesTimeTracker
                {
                    Id = id,
                    UserId = timeTracker.UserId,
                    Activity = timeTracker.Activity,
                    Description = timeTracker.Description,
                    Hours = timeTracker.Hours,
                    ProjectId = timeTracker.ProjectId,
                    ActivityDate = timeTracker.ActivityDate

                };
                return createdTimeTracker;
            }
        }
        public async Task<PresalesTimeTracker> UpdateAsync(PresalesTimeTracker timeTracker)
        {
            var query = "UPDATE PresalesTimeTracker SET UserId = @UserId, Activity = @Activity, Description = @Description, Hours = @Hours, ProjectId = @ProjectId, ActivityDate = @ActivityDate WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    timeTracker.Activity,
                    timeTracker.UserId,
                    timeTracker.Description,
                    timeTracker.Hours,
                    timeTracker.ActivityDate,
                    timeTracker.ProjectId,
                    timeTracker.Id
                });
                return timeTracker;
            }
        }
        public async Task<PresalesTimeTracker> DeleteAsync(Guid id)
        {
            var query = "UPDATE PresalesTimeTracker SET IsDestroyed = 1 WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });               
                var deletedTimeTracker = new PresalesTimeTracker { Id = id };
                return deletedTimeTracker;
            }
        }

    }
}
