using AutoMapper;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Handlers.Commands.CreateProjectTypeCommands;
using HexaPSA.Tool.Application.Handlers.Commands.TechnologyCommands;
using HexaPSA.Tool.Application.Handlers.Queries.ProjectQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.API.Controllers
{
    /// <summary>
    /// The Projects controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTypeContrller: ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTypeContrller"/> class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mapper"></param>
        public ProjectTypeContrller(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all of the project types.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of project types.</returns>
        [HttpGet("types")]
        [ProducesResponseType(typeof(List<ProjectTypeResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectTypes(CancellationToken cancellationToken)
        {
            var query = new GetProjectTypesQuery();

            var projectTypes = await _sender.Send(query, cancellationToken);

            return Ok(projectTypes);
        }

        /// <summary>
        /// Gets the project type with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The project type identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The project type with the specified identifier, if it exists.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProjectTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectTypeById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetProjectTypeByIdQuery(id);

            var projectType = await _sender.Send(query, cancellationToken);

            return Ok(projectType);
        }

        /// <summary>
        /// Creates a new project type based on the specified request.
        /// </summary>
        /// <param name="request">The create project type request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created project type.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProjectTypeResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProjectType([FromBody] CreateProjectTypeRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateProjectTypeCommand>(request);

            var createdProjectTYpe = await _sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetProjectTypeById), new { id = createdProjectTYpe.Id }, createdProjectTYpe);
        }

        /// <summary>
        /// Deletes a ProjectType record with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the ProjectType to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>No content if the ProjectType was successfully deleted, or a not found response if the record does not exist.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProjectType(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteProjectTypeCommands(id);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }


        /// <summary>
        /// Creates a new ProjectType record based on the specified request.
        /// </summary>
        /// <param name="id">The ID of the ProjectType to update.</param>
        /// <param name="request">The create ProjectType request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created ProjectType record.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProjectType(Guid id, [FromBody] UpdateProjectTypeRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateProjectTypeCommands(id, request.Name);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }

    }
}
