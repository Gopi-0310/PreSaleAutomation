using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Exceptions.Project;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;

namespace HexaPSA.Tool.Application.Handlers.Commands.CreateProjectTypeCommands
{
    public sealed record CreateProjectTypeCommand(string Name) : ICommand<ProjectTypeResponse>;
    public sealed record CreateProjectTypeRequest(string Name);
 
    public class CreateProjectTypeHandler : ICommandHandler<CreateProjectTypeCommand, ProjectTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProjectTypeRepository _projectTypeRepository;


        public CreateProjectTypeHandler(IMapper mapper, IProjectTypeRepository projectRepository)
        {
            _mapper = mapper;
            _projectTypeRepository = projectRepository;             
        }

        public async Task<ProjectTypeResponse> Handle(CreateProjectTypeCommand command, CancellationToken cancellationToken)
        {
            ProjectType projectType = new();

            //check if there's already a project type with the same name
            var exists = await _projectTypeRepository.GetByNameAsync(command.Name);
            if (exists != null)
                throw new ProjectTypeExistsException(command.Name);

            //map command to entity

            var item = _mapper.Map<ProjectType>(command);
            item.CreatedBy = new Guid();
            projectType = await _projectTypeRepository.AddAsync(item);

            return _mapper.Map<ProjectTypeResponse>(projectType);
        }
    }
}
