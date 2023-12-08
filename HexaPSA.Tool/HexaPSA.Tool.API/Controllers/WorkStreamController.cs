using AutoMapper;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Commands.WorkStreamCommands;
using HexaPSA.Tool.Application.Handlers.Queries.WorkStreamActivityQueries;
using HexaPSA.Tool.Application.Handlers.Queries.WorkStreamQueries;
using HexaPSA.Tool.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexaPSA.Tool.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// The Workstream controller.
    /// </summary>
    public sealed class WorkStreamController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkStreamController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        public WorkStreamController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the Workstream with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The Workstream identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The Workstream with the specified identifier</returns>

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(List<WorkStreamResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWorkStreamById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetWorkStreamByIdQuery(id); 

            var workstream = await _sender.Send(query, cancellationToken);

            return Ok(workstream);
        }

        /// <summary>
        /// Gets the Workstream Hours and week with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The Workstream identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The Workstream with the specified identifier</returns>

        [HttpGet("GetDetailsById/{id:guid}")]
        [ProducesResponseType(typeof(WorkStreamTotalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTotalWeekById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetByIdWorkStreamTotalWeeks(id);

            var workstreamHours = await _sender.Send(query, cancellationToken);

            return Ok(workstreamHours);
        }



        /// <summary>
        /// Gets all of the  Workstream.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of Workstream Details.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<WorkStreamResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllWorkStream(CancellationToken cancellationToken)
        {
            var query = new GetWorkStreamQuery();

            var workStream = await _sender.Send(query, cancellationToken);

            return Ok(workStream);
        }

        /// <summary>
        /// Creates a new  Workstream based on the specified request.
        /// </summary>
        /// <param name="request">The create  Workstream request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created  Workstream.</returns>

        [HttpPost]
        [ProducesResponseType(typeof(WorkStreamResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateWorkStream([FromBody] CreateWorkStreamRequest request, CancellationToken cancellationToken)
        {
            var createCommand = _mapper.Map<CreateWorkStreamCommands>(request);

            var CreateWorkStream = await _sender.Send(createCommand, cancellationToken);

            return CreatedAtAction(nameof(GetWorkStreamById), new { id = CreateWorkStream.Id }, CreateWorkStream);

        }


        /// <summary>
        /// Creates a new Workstream record based on the specified request.
        /// </summary>
        /// <param name="id">The ID of the workstream to update.</param>
        /// <param name="request">The create Workstream request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created Workstream record.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateWorkStream(Guid id, [FromBody] UpdateWorkStreamRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateWorkStreamCommands(id, request.Name,request.ProjectId,request.Orders);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Deletes a Workstream record with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the Workstream to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>No content if the Workstream was successfully deleted, or a not found response if the record does not exist.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWorkStream(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteWorkStreamCommand(id);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }


    }
}
