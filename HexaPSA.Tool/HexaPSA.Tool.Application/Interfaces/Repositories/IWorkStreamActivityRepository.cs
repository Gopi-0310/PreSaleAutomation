using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{
   
    public interface IWorkStreamActivityReadRepository : IReadRepository<WorkStreamActivity, Guid>
    {
        Task<List<WorkStreamActivityMappingResponse>> GetByIdAsync(Guid id);
        Task<List<WorkStreamActivityWeeksResponse>> GetByIdWeeks(Guid id);
        Task<WorkStreamActivity> GetByIdWorkStream(Guid id);
    }

    public interface IWorkStreamActivityWriteRepository : IWriteRepository<WorkStreamActivity, Guid>
    {

    }
    public interface IWorkStreamActivityRepository : IWorkStreamActivityReadRepository, IWorkStreamActivityWriteRepository
    {
       
    }
}
