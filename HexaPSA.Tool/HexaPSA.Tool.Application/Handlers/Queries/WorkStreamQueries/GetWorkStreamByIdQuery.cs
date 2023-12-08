using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;

namespace HexaPSA.Tool.Application.Handlers.Queries.WorkStreamQueries
{

    public sealed record GetWorkStreamByIdQuery(Guid Id) : IQuery<List<WorkStreamResponse>>;
    public sealed class GetWorkStreamByIdQueryHandler : IQueryHandler<GetWorkStreamByIdQuery,List<WorkStreamResponse>>
    {
        private readonly IWorkStreamRepository _workStreamRepository;
        private readonly IMapper _mapper;

        public GetWorkStreamByIdQueryHandler(IWorkStreamRepository workStreamRepository, IMapper mapper)
        {
            _workStreamRepository = workStreamRepository;
            _mapper = mapper;
        }

        public async Task<List<WorkStreamResponse>> Handle(GetWorkStreamByIdQuery request, CancellationToken cancellationToken)
        {
            var WorkStream = await _workStreamRepository.GetByIdAsync(request.Id);

            if (WorkStream is null)
            {
                //throw new RoleNotFoundException(request.Id);
            }

            return _mapper.Map<List<WorkStreamResponse>>(WorkStream);
        }
    }
}
