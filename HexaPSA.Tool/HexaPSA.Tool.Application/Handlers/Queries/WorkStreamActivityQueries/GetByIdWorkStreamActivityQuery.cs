using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Queries.WorkStreamActivityQueries
{
    
    public sealed record GetByIdWorkStreamActivityQuery(Guid Id) : IQuery<List<WorkStreamActivityMappingResponse>>;
    public sealed class GetWorkStreamActivityByIdQueryHandler : IQueryHandler<GetByIdWorkStreamActivityQuery,List<WorkStreamActivityMappingResponse>>
    {
        private readonly IWorkStreamActivityRepository _workStreamActivityRepository;
        private readonly IMapper _mapper;

        public GetWorkStreamActivityByIdQueryHandler(IWorkStreamActivityRepository workStreamActivityRepository, IMapper mapper)
        {
            _workStreamActivityRepository = workStreamActivityRepository;
            _mapper = mapper;
        }

        public async Task<List<WorkStreamActivityMappingResponse>> Handle(GetByIdWorkStreamActivityQuery request, CancellationToken cancellationToken)
        {
            var WorkStream = await _workStreamActivityRepository.GetByIdAsync(request.Id);

            if (WorkStream is null)
            {
                //throw new 
            }

            return _mapper.Map <List<WorkStreamActivityMappingResponse>>(WorkStream);
        }
    }
}
