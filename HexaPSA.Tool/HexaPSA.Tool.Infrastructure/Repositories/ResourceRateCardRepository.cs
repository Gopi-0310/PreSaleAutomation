using AutoMapper;
using Dapper;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using System.Data;

namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class ResourceRateCardRepository : IResourceRateCardRepository
    {
        private readonly DapperDataContext _context;
        private readonly IMapper _Mapper;
        public ResourceRateCardRepository(DapperDataContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }


        public async Task<IEnumerable<ResourceRate>> GetAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT *
                FROM ResourceRate r 
                INNER JOIN Role c ON r.RoleId = c.Id
                WHERE r.ProjectId IS NULL AND r.IsDestroyed = 0";
                var Resource = await connection.QueryAsync<ResourceRate, Role, ResourceRate>(sql, (resource, rate) => {
                    resource.Role = rate;
                    return resource;
                },
                splitOn: "Id,Id");
                return Resource;
            }
          
        }

        public async Task<ResourceRate> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM ResourceRate Where Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var item = await connection.QuerySingleOrDefaultAsync<ResourceRate>(query, new { id });
                return item;
            }
        }



        public async Task<ResourceRate> AddAsync(ResourceRate rateCard)
        {
            var query = "INSERT INTO ResourceRate (RoleId,ProjectId,Rate,CreatedDate,CreatedBy) OUTPUT inserted.Id VALUES (@RoleId,@ProjectId,@Rate, @CreatedDate, @CreatedBy)";

            var parameters = new DynamicParameters();
            parameters.Add("RoleId", rateCard.RoleId, DbType.Guid);
            parameters.Add("ProjectId", rateCard.ProjectId, DbType.Guid);
            parameters.Add("Rate", rateCard.Rate, DbType.Decimal);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
            parameters.Add("CreatedBy", rateCard.CreatedBy, DbType.Guid);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, parameters);
                var addRateCard = new ResourceRate
                {
                    Id = id,
                    ProjectId = rateCard.ProjectId,
                    RoleId = rateCard.RoleId,
                    Rate = rateCard.Rate

                };
                return addRateCard;
            }
        }


        public async Task<ResourceRate> UpdateAsync(ResourceRate rateCard)
        {
            var query = "UPDATE ResourceRate SET RoleId = @RoleId, ProjectId = @ProjectId, Rate = @Rate WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, rateCard);
                return rateCard;
            }
        }



        public async Task<ResourceRate> DeleteAsync(Guid id)
        {
            var rateCard = await GetByIdAsync(id);
            if(rateCard != null)
            {
                var query = "UPDATE ResourceRate SET IsDestroyed = 1 WHERE Id = @id";
                using (var connection = _context.CreateConnection())
                {
                    var changesRow = await connection.ExecuteAsync(query, new { id });
                }
            }
            var deletedRateCard = new ResourceRate { Id = id };
            return deletedRateCard;
        }
    }
}