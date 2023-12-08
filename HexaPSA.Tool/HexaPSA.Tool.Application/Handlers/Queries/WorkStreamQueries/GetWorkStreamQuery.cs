using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Queries.WorkStreamQueries
{

    public sealed record GetWorkStreamQuery : IQuery<List<WorkStreamResponse>>;
    internal sealed class GetWorkStreamQueryHandler : IQueryHandler<GetWorkStreamQuery, List<WorkStreamResponse>>
    {
        private readonly IWorkStreamRepository _workStreamRepository;
        private readonly IMapper _mapper;

        public GetWorkStreamQueryHandler(IWorkStreamRepository workStreamRepository, IMapper mapper)
        {
            _workStreamRepository = workStreamRepository;
            _mapper = mapper;
        }

        public async Task<List<WorkStreamResponse>> Handle(GetWorkStreamQuery request, CancellationToken cancellationToken)
        {
            var roles = await _workStreamRepository.GetAllAsync();

            return _mapper.Map<List<WorkStreamResponse>>(roles);
        }
    }
}
