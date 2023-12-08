using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Technology;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static GetAllTechnologyQueryHandler;

public sealed class GetAllTechnologyQueryHandler : IQueryHandler<GetAllTechnologyQuery, List<TechnologyDropResponse>>
{
    public sealed record GetAllTechnologyQuery : IQuery<List<TechnologyDropResponse>>;

    private readonly ITechnologyRepository _technologyRepository;
    private readonly IMapper _mapper;

    public GetAllTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
    {
        _technologyRepository = technologyRepository;
        _mapper = mapper;
    }

    public async Task<List<TechnologyDropResponse>> Handle(GetAllTechnologyQuery request, CancellationToken cancellationToken)
    {
        var technologies = await _technologyRepository.GetAllAsync();

        return _mapper.Map<List<TechnologyDropResponse>>(technologies);
    }
}
