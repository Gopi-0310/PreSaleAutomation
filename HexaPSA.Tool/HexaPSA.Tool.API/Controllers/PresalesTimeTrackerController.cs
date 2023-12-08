using AutoMapper;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Handlers.Commands.PresaleTimeTrakerCommands;
using HexaPSA.Tool.Application.Handlers.Queries.PresaleTimeTrackerQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CreatePresalesTimeTrackerHandler;
using static GetAllPresalesTimeTrackersQueryHandler;

namespace HexaPSA.Tool.API.Controllers
{
    /// <summary>
    /// The PresalesTimeTrackerController controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class PresalesTimeTrackerController:ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mapper"></param>
        public PresalesTimeTrackerController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets all of the PresalesTimeTracker.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of PresalesTimeTracker.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PresalesTimeTrackerResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPresalesTimeTrackers(CancellationToken cancellationToken)
        {
            var query = new GetAllPresalesTimeTrackersQuery();

            var presalesTimeTrackers = await _sender.Send(query, cancellationToken);

            return Ok(presalesTimeTrackers);
        }
        /// <summary>
        /// Gets the project type with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The project type identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The project type with the specified identifier, if it exists.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PresalesTimeTrackerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPresalesTimeTrackerById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetPresalesTimeTrackerByIdQuery(id);

            var presalesTimeTracker = await _sender.Send(query, cancellationToken);

            return Ok(presalesTimeTracker);
        }
        /// <summary>
        /// Creates a new project type based on the specified request.
        /// </summary>
        /// <param name="request">The create project type request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created project type.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PresalesTimeTrackerResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePresalesTimeTracker([FromBody] CreatePresalesTimeTrackerRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreatePresalesTimeTrackerCommand>(request);

            var createdPresalesTimeTracker = await _sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetPresalesTimeTrackerById), new { id = createdPresalesTimeTracker.Id }, createdPresalesTimeTracker);
        }
        /// <summary>
        /// Creates a new PresalesTimeTracker record based on the specified request.
        /// </summary>
        /// <param name="request">The create PresalesTimeTracker request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created PresalesTimeTracker record.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePresalesTimeTracker(Guid id, [FromBody] UpdatePresalesTimeTrackerRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdatePresalesTimeTrackerCommand(id,request.UserId,request.ProjectId, request.Activity, request.Description, request.Hours,request.ActivityDate);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }
        /// <summary>
        /// Deletes a PresalesTimeTracker record with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the PresalesTimeTracker to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>No content if the PresalesTimeTracker was successfully deleted, or a not found response if the record does not exist.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePresalesTimeTracker(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeletePresalesTimeTrackerCommand(id);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }

    }
}
