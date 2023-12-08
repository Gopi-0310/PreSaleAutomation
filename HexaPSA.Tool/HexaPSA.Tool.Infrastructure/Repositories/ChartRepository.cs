using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using Dapper;
using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Interfaces.Common;

namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class ChartRepository : IChartRepository
    {
        private readonly DapperDataContext _context;

        public ChartRepository(DapperDataContext context)
        {
            _context = context;
        }

        public Task<ChartResponse> AddAsync(ChartResponse item)
        {
            throw new NotImplementedException();
        }

        public Task<ChartResponse> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ChartResponse>> GetAllAsync()
        {
            var query = @"
                       SELECT
                       p.Id,
                       p.Name,
                       SUM(W.Hours) AS Hours
                       FROM WorkStreamActivity W 
                       JOIN WorkStream ww ON W.WorkStreamActivityId = ww.Id 
                       JOIN Project p ON ww.ProjectId = p.Id 
                       WHERE W.IsDestroyed = 0
                       GROUP BY p.Id, p.Name";
            using (var connection = _context.CreateConnection())
            {    
                var chart = await connection.QueryAsync<ChartResponse, TotalHoursResponse, ChartResponse>(
                query,
                (chartres, thr) =>
                {
                    chartres.Hours = thr.Hours.ToString();
                   
                    return chartres;
                },
                splitOn: "Hours"
                );
                return (List<ChartResponse>)chart;
            }
        }

        public Task<ChartResponse> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ChartResponse> UpdateAsync(ChartResponse item)
        {
            throw new NotImplementedException();
        }
    }
}
