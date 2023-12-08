using AutoMapper;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Commands.CapacityUtilizationCommands;
using HexaPSA.Tool.Application.Handlers.Commands.CapacityUtilizationsCommands;
using HexaPSA.Tool.Application.Handlers.Commands.CreateProjectTypeCommands;
using HexaPSA.Tool.Application.Handlers.Commands.ProjectCommands;
using HexaPSA.Tool.Application.Handlers.Queries.ProjectQueries;
using HexaPSA.Tool.Application.Handlers.Queries.ProjectQuery;
using HexaPSA.Tool.Application.Handlers.Queries.RoleQueries;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CreatePresalesTimeTrackerHandler;
using static GetAllProjectQueryHandler;

namespace HexaPSA.Tool.API.Controllers
{
    /// <summary>
    /// The Projects controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ProjectsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mapper"></param>
        public ProjectsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all projects.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A list of projects or a Not Found response if no projects are found.
        /// </returns>
        [HttpGet("GetAllProject")]
        [ProducesResponseType(typeof(ProjectResponseAll), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllProject(CancellationToken cancellationToken)
        {
            var query = new GetAllProjectQuery();
            var allProject = await _sender.Send(query, cancellationToken);
            return Ok(allProject);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController"/>
        /// </summary>
        [HttpGet("GetProject/{id:guid}")]
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetProjectQuery(id);

            var project = await _sender.Send(query, cancellationToken);

            return Ok(project);
        }
      
        [HttpPost("CreateProject")]
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateProjectCommand>(request);
            var createdProject = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
        }

        [HttpPut("EditProject/{id:guid}")]
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditProject(Guid id, [FromBody] CreateProjectRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateProjectCommand>(request);
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }


        [HttpDelete("DeleteProject/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProject(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteProjectCommand(id);
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpGet("GetAllChart")]
        [ProducesResponseType(typeof(ChartResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetChartAll(CancellationToken cancellationToken)
        {
            var query = new GetChartAllQuery();

            var chart = await _sender.Send(query, cancellationToken);

            return Ok(chart);
        }

        [HttpGet("GetAllGanttChart")]
        [ProducesResponseType(typeof(GanttChartResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGanttChartAll(CancellationToken cancellationToken)
        {
            var query = new GetGanttChartAllQuery();
            var ganttChart = await _sender.Send(query, cancellationToken);
            return Ok(ganttChart);
        }

    }
}
