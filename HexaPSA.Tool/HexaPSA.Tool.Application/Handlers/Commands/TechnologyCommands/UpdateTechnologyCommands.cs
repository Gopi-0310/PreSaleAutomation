using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Commands.TechnologyCommands
{
    public sealed record UpdateTechnologyCommands(Guid Id, string Name) : ICommand<Technology>;
    public sealed record UpdateTechnologyRequest(string Name);

    public class UpdateTechnologyHandler : ICommandHandler<UpdateTechnologyCommands, Technology>
    {
        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;

        public UpdateTechnologyHandler(IMapper mapper, ITechnologyRepository technologyRepository)
        {
            _mapper = mapper;
            _technologyRepository = technologyRepository;
        }

        public async Task<Technology> Handle(UpdateTechnologyCommands command, CancellationToken cancellationToken)
        {
            // Check if the Technology with the specified ID exists
            var existingTechnology = await _technologyRepository.GetByIdAsync(command.Id);
            if (existingTechnology == null)
            {
                // Handle the case where the Technology with the specified ID does not exist
                // You can throw an exception or return an appropriate response
            }

            // Update the Technology properties
            existingTechnology.Name = command.Name;

            // Save the updated Technology to the repository
            await _technologyRepository.UpdateAsync(existingTechnology);

            return _mapper.Map<Technology>(existingTechnology);
        }
    }
}
