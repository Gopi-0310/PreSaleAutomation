using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Contracts.Technology;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using static CreateTechnologyHandler;

public class CreateTechnologyHandler : ICommandHandler<CreateTechnologyCommand, TechnologyDropResponse>
{

   
    public sealed record CreateTechnologyCommand(string Name) : ICommand<TechnologyDropResponse>;
    public sealed record CreateTechnologyRequest(string Name);


    private readonly IMapper _mapper;
    private readonly ITechnologyRepository _technologyRepository;

    public CreateTechnologyHandler(IMapper mapper, ITechnologyRepository technologyRepository)
    {
        _mapper = mapper;
        _technologyRepository = technologyRepository;
    }

    public async Task<TechnologyDropResponse> Handle(CreateTechnologyCommand command, CancellationToken cancellationToken)
    {
        // Map command to entity
        var technology = _mapper.Map<Technology>(command);

        
        var createdTechnology = await _technologyRepository.AddAsync(technology);

        return _mapper.Map<TechnologyDropResponse>(createdTechnology);
    }
}
