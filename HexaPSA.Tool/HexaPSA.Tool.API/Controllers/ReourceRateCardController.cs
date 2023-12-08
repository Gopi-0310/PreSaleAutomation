using AutoMapper;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Commands.ResourceRateCommands;
using HexaPSA.Tool.Application.Handlers.Queries.ResourceRateQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexaPSA.Tool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// The RerourceRateCard controller.
    /// </summary>
    public sealed class ResourceRateCardController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceRateCardController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        public ResourceRateCardController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the ResourceRateCard with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The RateCard identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The RateCard with the specified identifier</returns>

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ResourceRateCardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRateCardById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetResourceRateCardByIdQuery(id);

            var rateCard = await _sender.Send(query, cancellationToken);

            return Ok(rateCard);
        }

        /// <summary>
        /// Gets all of the  RateCard.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of RateCard Details.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ResourceRateCardResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRateCard(CancellationToken cancellationToken)
        {
            var query = new GetAllResourceRateCardQuery();

            var rateCard = await _sender.Send(query, cancellationToken);

            return Ok(rateCard);
        }

        /// <summary>
        /// Creates a new  RateCard based on the specified request.
        /// </summary>
        /// <param name="request">The create  RateCard request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created  RateCard.</returns>

        [HttpPost]
        [ProducesResponseType(typeof(ResourceRateCardResponseRoleId), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRateCard([FromBody] CreateResourceRateCardRequest request, CancellationToken cancellationToken)
        {
                var createCommand = _mapper.Map<CreateResourceRateCardCommand>(request);

                var CreateRateCard = await _sender.Send(createCommand, cancellationToken);

                return CreatedAtAction(nameof(GetRateCardById), new { Id = CreateRateCard.Id }, CreateRateCard);
           
        }


        /// <summary>
        /// Creates a new RateCard record based on the specified request.
        /// </summary>
        /// <param name="request">The create RateCard request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created RateCard record.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateResourceRateCard(Guid id, [FromBody] UpdateResourceRateCardRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateResourceRateCardCommand (id, request.RoleId, request.Rate);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Deletes a ResourceRateCard record with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the ResourceRateCard to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>No content if the ResourceRateCard was successfully deleted, or a not found response if the record does not exist.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteResourceRate(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteResourceRateCardCommand(id);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }

        
    }
}
