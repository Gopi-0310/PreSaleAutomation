using AutoMapper;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Handlers.Commands.RoleCommands;
using HexaPSA.Tool.Application.Handlers.Queries.RoleQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexaPSA.Tool.API.Controllers
{
    /// <summary>
    /// The RoleController controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class RoleController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mapper"></param>
        public RoleController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets the project type with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The project type identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The project type with the specified identifier, if it exists.</returns>

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoleById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetRoleByIdQuery(id);

            var role = await _sender.Send(query, cancellationToken);

            return Ok(role);
        }
        /// <summary>
        /// Gets all of the Role.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of Role.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<RoleResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
        {
            var query = new GetAllRolesQuery();

            var roles = await _sender.Send(query, cancellationToken);

            return Ok(roles);
        }
        /// <summary>
        /// Creates a new project type based on the specified request.
        /// </summary>
        /// <param name="request">The create project type request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created project type.</returns>

        [HttpPost]
        [ProducesResponseType(typeof(RoleResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateRoleCommand>(request);

            var createdRole = await _sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetRoleById), new { roleId = createdRole.Id }, createdRole);
        }
        /// <summary>
        /// Updates an existing Role with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the Role to update.</param>
        /// <param name="request">The updated Role information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>No content if the update is successful, or a not found response if the Role does not exist.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateRoleCommand(id, request.Name, request.Code);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }
        /// <summary>
        /// Deletes a Role with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the Role to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>No content if the deletion is successful, or a not found response if the Role does not exist.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRole(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteRoleCommand(id);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
