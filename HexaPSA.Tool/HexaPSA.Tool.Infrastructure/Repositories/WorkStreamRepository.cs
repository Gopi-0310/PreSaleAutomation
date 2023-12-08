using Dapper;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using System.Data;

namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class WorkStreamRepository : IWorkStreamRepository
    {
        private readonly DapperDataContext _context;
        public WorkStreamRepository(DapperDataContext context)
        {
            _context = context;
        }

        public async Task<WorkStream> AddAsync(WorkStream workStream)
        {
            var orderBy = 0;
            var workStreamLists = await GetByIdAsync(workStream.ProjectId);
            if (workStreamLists.Any())
            {
                var maxOrder = workStreamLists.Max(ws => ws.Orders);
                orderBy = maxOrder + 1;
            }
            else
            {
                orderBy = 1;
            }
            var query = "INSERT INTO WorkStream (Name,ProjectId,CreatedDate,CreatedBy,Orders) OUTPUT inserted.Id VALUES (@Name,@ProjectId,@CreatedDate, @CreatedBy, @Orders)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", workStream.Name, DbType.String);
            parameters.Add("ProjectId", workStream.ProjectId, DbType.Guid);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
            parameters.Add("CreatedBy", workStream.CreatedBy, DbType.Guid);
            parameters.Add("Orders", orderBy, DbType.Int16);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, parameters);
                var AddWorkStream = new WorkStream
                {
                    Id = id,
                    Name = workStream.Name,
                    ProjectId = workStream.ProjectId,
                    Orders = orderBy
                };
                return AddWorkStream;


            }
        }

        public async Task<WorkStream> DeleteAsync(Guid id)
        {
            var workStream = await GetByIdDataAsync(id);
            if (workStream != null)
            {
                var query = "UPDATE WorkStream SET IsDestroyed = 1 WHERE Id = @id";
                using (var connection = _context.CreateConnection())
                {
                    var changesRow = await connection.ExecuteAsync(query, new { id });
                }
            }
            var deletedResponce = new WorkStream { Id = id };
            return deletedResponce;
        }

        public async Task<IEnumerable<WorkStream>> GetAllAsync()
        {
            var query = "SELECT * FROM WorkStream WHERE IsDestroyed = 0";
            using (var connection = _context.CreateConnection())
            {
                var items = await connection.QueryAsync<WorkStream>(query);
                return items;

            }
        }

        public async Task<List<WorkStreamResponse>> GetByIdAsync(Guid ProjectId)
        {
            var query = "SELECT * FROM WorkStream WHERE ProjectId = @ProjectId";
            using (var connection = _context.CreateConnection())
            {

                var item = await connection.QueryAsync<WorkStreamResponse>(query, new { ProjectId });
                var sortedItems = item.OrderBy(item => item.Orders).ToList();
                return sortedItems;
            }
        }

        public async Task<WorkStreamResponse> GetByIdDataAsync(Guid id)
        {
            var query = "SELECT * FROM WorkStream WHERE Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var workStream = await connection.QuerySingleOrDefaultAsync<WorkStreamResponse>(query, new { id });
                return workStream;
            }
        }

        public async Task<WorkStream> UpdateAsync(WorkStream item)
        {
            var query = "UPDATE WorkStream SET Name = @Name,ProjectId = @ProjectId,Orders = @Orders WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    item.Id,
                    item.Name,
                    item.ProjectId,
                    item.Orders

                });
                return item;
            }
        }

        public async Task<WorkStreamResponse> UpdateAsync(WorkStreamResponse existingWorksStream)
        {
            var query = "UPDATE WorkStream SET Name = @Name,ProjectId = @ProjectId,Orders = @Orders WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    existingWorksStream.Id,
                    existingWorksStream.Name,
                    existingWorksStream.ProjectId,
                    existingWorksStream.Orders

                });
                return existingWorksStream;
            }
        }

        Task<WorkStream> IReadRepository<WorkStream, Guid>.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

       
    }
}
