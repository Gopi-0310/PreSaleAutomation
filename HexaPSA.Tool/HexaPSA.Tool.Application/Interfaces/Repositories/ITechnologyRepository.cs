using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{

    public interface ITechnologyReadRepository : IReadRepository<Technology, Guid>
        {
            Task<Technology> GetByNameAsync(string name);
        }

        public interface ITechnologyWriteRepository : IWriteRepository<Technology, Guid>
        {
             
        }

        public interface ITechnologyRepository : ITechnologyReadRepository, ITechnologyWriteRepository
        {

        }
    
}
