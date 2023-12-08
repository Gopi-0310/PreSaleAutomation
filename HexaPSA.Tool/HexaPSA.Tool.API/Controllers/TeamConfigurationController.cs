using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Queries.TeamConfigurationQueries;
using HexaPSA.Tool.Application.Handlers.Queries.CapacityUtilizationQueries;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Application.Handlers.Commands.CapacityUtilizationsCommands;
using HexaPSA.Tool.Application.Contracts;
using static GetAllPresalesTimeTrackersQueryHandler;
using HexaPSA.Tool.Application.Handlers.Commands.TeamConfigurationCommands;
using HexaPSA.Tool.Application.Handlers.Commands.ResourceRateCommands;
using HexaPSA.Tool.Application.Handlers.Commands.RoleCommands;

namespace HexaPSA.Tool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class TeamConfigurationController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public TeamConfigurationController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets the TeamConfiguration detail Role details and Role details with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The teamConfigurations identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The teamConfigurations with the specified identifier, if it exists.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TeamConfigurationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetByIdTeamConfigurationQuery(id);
            var teamConfigurations = await _sender.Send(query, cancellationToken);
            return Ok(teamConfigurations);
        }

        [HttpGet("{id:guid}/id")]
        [ProducesResponseType(typeof(TeamConfigResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeamByIdAll(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetTeamByIdTeamConfigurationQuery(id);
            var teamConfigurations = await _sender.Send(query, cancellationToken);
            return Ok(teamConfigurations);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TeamConfigResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTeamConfigurations([FromBody] CreateTeamConfigurationsRequest request, CancellationToken cancellationToken)
        {
            var createCommand = _mapper.Map<CreateTeamConfigurationsCommand>(request);
            var teamConfigurations = await _sender.Send(createCommand, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { Id = teamConfigurations.Id }, teamConfigurations);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTeamConfigurations(Guid id, [FromBody] UpdateTeamConfigurationsRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateTeamConfigurationsCommand(id, request.UserId, request.ProjectId, request.RoleId);
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTeamConfigurations(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteTeamConfigurationsCommand(id);
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
