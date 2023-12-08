using AutoMapper;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Contracts.Technology;
using HexaPSA.Tool.Application.Handlers.Commands.CreateProjectTypeCommands;
using HexaPSA.Tool.Application.Handlers.Commands.TechnologyCommands;
using HexaPSA.Tool.Application.Handlers.Queries.TechnologyQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static CreatePresalesTimeTrackerHandler;
using static CreateTechnologyHandler;
using static GetAllTechnologyQueryHandler;

namespace HexaPSA.Tool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class TechnologyController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public TechnologyController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TechnologyDropResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTechnologies(CancellationToken cancellationToken)
        {
            var query = new GetAllTechnologyQuery();
            var technologies = await _sender.Send(query, cancellationToken);
            return Ok(technologies);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TechnologyDropResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTechnologyById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetTechnologyByIdQuery(id);
            var technology = await _sender.Send(query, cancellationToken);
            return Ok(technology);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TechnologyDropResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTechnology([FromBody] CreateTechnologyRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateTechnologyCommand>(request);
            var createdTechnology = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetTechnologyById), new { id = createdTechnology.Id }, createdTechnology);
        }


        /// <summary>
        /// Deletes a Technology record with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the Technology to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>No content if the Technology was successfully deleted, or a not found response if the record does not exist.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWorkTechnology(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteTechnologyCommands(id);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Creates a new Technology record based on the specified request.
        /// </summary>
        /// <param name="id">The ID of the technology to update.</param>
        /// <param name="request">The create technology request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created technology record.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTechnology(Guid id, [FromBody] UpdateTechnologyRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateTechnologyCommands(id, request.Name);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }

    }
}
