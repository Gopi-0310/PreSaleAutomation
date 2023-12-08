using Dapper;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class WorkStreamActivityRepository : IWorkStreamActivityRepository
    {

        private readonly DapperDataContext _context;
        public WorkStreamActivityRepository(DapperDataContext context)
        {
            _context = context;
        }

        public async Task<WorkStreamActivity> AddAsync(WorkStreamActivity workStreamActivity)
        {
            if(workStreamActivity.ParentId != null)
            {
                var id = workStreamActivity.ParentId;
                var workStream = await GetByIdAsync((Guid)id);
                var querys = "UPDATE WorkStreamActivity SET Hours = Null,Week = Null WHERE Id = @id";
                using (var connection = _context.CreateConnection())
                {
                    var changesRow = await connection.ExecuteAsync(querys, new { id });
                }
            }
            var query = "INSERT INTO WorkStreamActivity (Activity,RoleId,WorkStreamActivityId,Hours,Week,CreatedDate,CreatedBy,Description,ParentId) OUTPUT inserted.Id VALUES (@Activity,@RoleId,@WorkStreamActivityId,@Hours,@Week,@CreatedDate,@CreatedBy,@Description,@ParentId)";

            var parameters = new DynamicParameters();
            parameters.Add("Activity", workStreamActivity.Activity, DbType.String);
            parameters.Add("RoleId", workStreamActivity.RoleId, DbType.Guid);
            parameters.Add("WorkStreamActivityId", workStreamActivity.WorkStreamActivityId, DbType.Guid);
            parameters.Add("Hours", workStreamActivity.Hours, DbType.Double);
            parameters.Add("Week", workStreamActivity.Week, DbType.Double);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
            parameters.Add("CreatedBy", workStreamActivity.CreatedBy, DbType.Guid);
            parameters.Add("Description", workStreamActivity.Description, DbType.String);
            parameters.Add("ParentId", workStreamActivity.ParentId, DbType.Guid);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, parameters);
                var AddWorkStream = new WorkStreamActivity
                {
                    Id = id,
                    Activity = workStreamActivity.Activity,
                    RoleId = workStreamActivity.RoleId,
                    WorkStreamActivityId = workStreamActivity.WorkStreamActivityId,
                    Hours = workStreamActivity.Hours,
                    Week = workStreamActivity.Week,
                    Description = workStreamActivity.Description,
                    ParentId = workStreamActivity.ParentId
                };
                return AddWorkStream;

            }
        }

        public async Task<WorkStreamActivity> DeleteAsync(Guid id)
        {
            var workStream = await GetByIdAsync(id);
            if (workStream != null)
            {
                var query = "UPDATE WorkStreamActivity SET IsDestroyed = 1 WHERE Id = @id";
                using (var connection = _context.CreateConnection())
                {
                    var changesRow = await connection.ExecuteAsync(query, new { id });
                }
            }
            var deletedResponce = new WorkStreamActivity { Id = id };
            return deletedResponce;
        }

        public Task<IEnumerable<WorkStreamActivityMappingResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

       

        public async Task<List<WorkStreamActivityMappingResponse>> GetByIdAsync(Guid id)
        {
            var query = "SELECT W.Id,W.Activity,W.WorkStreamActivityId,W.Hours,W.Week,W.Description,W.ParentId,W.CreatedDate AS StartDate,r.Id ,r.Code,r.Name ,ww.Id ,ww.Name,ww.ProjectId,rr.Rate,rr.Id FROM  WorkStreamActivity W JOIN Role r ON W.RoleId = r.Id JOIN WorkStream ww ON W.WorkStreamActivityId = ww.Id JOIN ResourceRate rr ON ww.ProjectId = rr.ProjectId AND W.RoleId = rr.RoleId WHERE  ww.ProjectId = @id AND W.IsDestroyed = 0";
            using (var connection = _context.CreateConnection())
            {
                var workStreamActivity = await connection.QueryAsync<WorkStreamActivityMappingResponse, RoleResponse, WorkStreamMappingResponse, ResourceRateResponseMapping, WorkStreamActivityMappingResponse>(
                query,
                (workstream, role, activity, resorce) =>
                {
                    workstream.Role = role;
                    workstream.WorkStream = activity;
                    workstream.Rate = resorce;
                    return workstream;
                },
                new { id }, // Properly binding the parameter
                splitOn: "Id,RoleId,WorkStreamActivityId,Id,Id,Id,Rate"
                );
                return (List<WorkStreamActivityMappingResponse>)workStreamActivity;
            }
        }

        public async Task<List<WorkStreamActivityWeeksResponse>> GetByIdWeeks(Guid id)
        {
            var query = "SELECT ws.Id,ws.Name,ws.ProjectId,p.Id,p.Name,p.EffectiveStartDate,p.EffectiveEndDate,wsa.Hours,wsa.Week,wsa.WorkStreamActivityId FROM WorkStream ws INNER JOIN Project p ON ws.ProjectId = p.Id INNER JOIN WorkStreamActivity wsa ON ws.Id = wsa.WorkStreamActivityId WHERE ws.ProjectId = @id;";
            using (var connection = _context.CreateConnection())
            {
                var workStreamActivity = await connection.QueryAsync<WorkStreamActivityWeeksResponse, WorkStreamProjectResponse, WorkStreamActivityWeeksResponse>(
                query,
                (workstream, project) =>
                {
                    workstream.Project = project;
                    
                    return workstream;
                },
                new { id }, // Properly binding the parameter
                splitOn: "Id,Id,Id"
                );
                return (List<WorkStreamActivityWeeksResponse>)workStreamActivity;
            }
        }

        public async Task<WorkStreamActivity> GetByIdWorkStream(Guid id)
        {
            var query = "SELECT * FROM WorkStreamActivity WHERE Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var workStream = await connection.QuerySingleOrDefaultAsync<WorkStreamActivity>(query, new { id });
                return workStream;
            }
        }

        
        public  async Task<WorkStreamActivity> UpdateAsync(WorkStreamActivity item)
        {
            var query = "UPDATE WorkStreamActivity SET Activity = @Activity,RoleId = @RoleId,WorkStreamActivityId = @WorkStreamActivityId, Hours = @Hours, Week = @Week,Description = @Description,ParentId = @ParentId WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    item.Id,
                    item.Activity,
                    item.RoleId,
                    item.WorkStreamActivityId,
                    item.Hours,
                    item.Week,
                    item.Description,
                    item.ParentId
                });
                return item;
            }
        }

        Task<IEnumerable<WorkStreamActivity>> IReadRepository<WorkStreamActivity, Guid>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<WorkStreamActivity> IReadRepository<WorkStreamActivity, Guid>.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}