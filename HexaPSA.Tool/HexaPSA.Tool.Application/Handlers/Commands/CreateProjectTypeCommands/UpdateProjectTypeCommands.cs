using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Commands.CreateProjectTypeCommands
{
   
    public sealed record UpdateProjectTypeCommands(Guid Id, string Name) : ICommand<ProjectType>;
    public sealed record UpdateProjectTypeRequest(string Name);

    public class UpdateProjectTypeHandler : ICommandHandler<UpdateProjectTypeCommands, ProjectType>
    {
        private readonly IMapper _mapper;
        private readonly IProjectTypeRepository _projectTypeRepository;

        public UpdateProjectTypeHandler(IMapper mapper, IProjectTypeRepository projectTypeRepository)
        {
            _mapper = mapper;
            _projectTypeRepository = projectTypeRepository;
        }

        public async Task<ProjectType> Handle(UpdateProjectTypeCommands command, CancellationToken cancellationToken)
        {
            // Check if the ProjectType with the specified ID exists
            var existingProjectType = await _projectTypeRepository.GetByIdAsync(command.Id);
            if (existingProjectType == null)
            {
                // Handle the case where the ProjectType with the specified ID does not exist
                // You can throw an exception or return an appropriate response
            }

            // Update the ProjectType properties
            existingProjectType.Name = command.Name;

            // Save the updated ProjectType to the repository
            await _projectTypeRepository.UpdateAsync(existingProjectType);

            return _mapper.Map<ProjectType>(existingProjectType);
        }
    }
}
