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
    public sealed record GetAllWorkStreamActivityQuery : IQuery<List<WorkStreamActivityResponse>>;
    internal sealed class GetWorkStreamActivityQueryHandler : IQueryHandler<GetAllWorkStreamActivityQuery, List<WorkStreamActivityResponse>>
    {
        private readonly IWorkStreamActivityRepository _workStreamActivityRepository;
        private readonly IMapper _mapper;

        public GetWorkStreamActivityQueryHandler(IWorkStreamActivityRepository workStreamRepository, IMapper mapper)
        {
            _workStreamActivityRepository = workStreamRepository;
            _mapper = mapper;
        }

        public async Task<List<WorkStreamActivityResponse>> Handle(GetAllWorkStreamActivityQuery request, CancellationToken cancellationToken)
        {
            var roles = await _workStreamActivityRepository.GetAllAsync();

            return _mapper.Map<List<WorkStreamActivityResponse>>(roles);
        }
    }
}
