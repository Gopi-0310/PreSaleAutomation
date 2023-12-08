using AutoMapper;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Commands.CapacityUtilizationCommands;
using HexaPSA.Tool.Application.Handlers.Commands.CapacityUtilizationsCommands;
using HexaPSA.Tool.Application.Handlers.Commands.ResourceRateCommands;
using HexaPSA.Tool.Application.Handlers.Queries.CapacityUtilizationQueries;
using HexaPSA.Tool.Application.Handlers.Queries.RoleQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexaPSA.Tool.API.Controllers
{/// <summary>
/// CapacityUtilization
/// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class CapacityUtilizationController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="CapacityUtilizationController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        public CapacityUtilizationController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets the project detail and role details and capacity details with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The capacity identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The capacityUtilization with the specified identifier, if it exists.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CapacityMappingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetByIdCapacityUtilizationQuery(id);
            var capacity = await _sender.Send(query, cancellationToken);
            return Ok(capacity);
        }

        /// <summary>
        /// Gets all of CapacityUtilization
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection Capacity Data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CapacityMappingResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCapacityUtilization(CancellationToken cancellationToken)
        {
            var query = new GetCapacityUtilizationQuery();
            var capacity = await _sender.Send(query, cancellationToken);
            return Ok(capacity);
        }




        /// <summary>
        /// Creates a new  Capacity based on the specified request.
        /// </summary>
        /// <param name="request">The create Capacity request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created Capacity Utilization.</returns
        
        [HttpPost]
        [ProducesResponseType(typeof(CapacityUtilizationResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCapacityUtilization([FromBody] CreateCapacityUtilizationRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateCapacityUtilizationCommand>(request);
            var createdCapacity = await _sender.Send(command, cancellationToken);
            return Ok();
        }
        /// <summary>
        /// Update Capacity on the specified request.
        /// </summary>
        /// <param name="request">The Update Capacity request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The Updated Capacity.</returns>
        /// 





        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCapacityUtilization(Guid id, [FromBody] UpdateCapacityUtilizationRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateCapacityUtilizationCommand(id, request.RoleId, request.ResourceId, request.ProjectId, request.Hours, request.Rate,request.Location);
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }






        /// <summary>
        /// Deletes a CapacityUtilization record with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the CapacityUtilization to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>No content if the CapacityUtilization  was successfully deleted, or a not found response if the record does not exist.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCapacityUtilization(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCapacityUtilizationCommand(id);
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }
    }
}