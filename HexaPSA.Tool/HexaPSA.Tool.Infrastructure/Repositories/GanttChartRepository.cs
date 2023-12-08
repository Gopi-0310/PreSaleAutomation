using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Interfaces.Common;

namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class GanttChartRepository : IGanttChartRepository
    {
        private readonly DapperDataContext _context;

        public GanttChartRepository(DapperDataContext context)
        {
            _context = context;
        }

        public Task<GanttChartResponse> AddAsync(GanttChartResponse item)
        {
            throw new NotImplementedException();
        }

        public Task<GanttChartResponse> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GanttChartResponse>> GetAllAsync()
        {
            var query = @"
        SELECT
            p.Id,
            p.Name,
            SUM(DATEDIFF(HOUR, p.EffectiveStartDate, p.EffectiveEndDate)) AS Hours,
            SUM(DATEDIFF(DAY, p.EffectiveStartDate, p.EffectiveEndDate)) AS Days
            FROM Project p
            WHERE p.EffectiveStartDate IS NOT NULL
            AND p.EffectiveEndDate IS NOT NULL
            GROUP BY p.Id, p.Name";

            using (var connection = _context.CreateConnection())
            {
                var chart = await connection.QueryAsync<GanttChartResponse>(query);
                return chart;
            }
        }

        public Task<GanttChartResponse> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GanttChartResponse> UpdateAsync(GanttChartResponse item)
        {
            throw new NotImplementedException();
        }
    }
}
