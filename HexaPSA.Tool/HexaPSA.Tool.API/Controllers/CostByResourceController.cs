using AutoMapper;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Commands.CapacityUtilizationCommands;
using HexaPSA.Tool.Application.Handlers.Commands.CapacityUtilizationsCommands;
using HexaPSA.Tool.Application.Handlers.Commands.ResourceRateCommands;
using HexaPSA.Tool.Application.Handlers.Queries.CapacityUtilizationQueries;
using HexaPSA.Tool.Application.Handlers.Queries.RoleQueries;
using HexaPSA.Tool.Application.Handlers.Queries.WorkStreamActivityQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexaPSA.Tool.API.Controllers
{/// <summary>
/// CostByResourceController
/// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class CostByResourceController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="CostByResourceController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        public CostByResourceController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the costByResource with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The costByResource identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The costByResource with the specified identifier</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(WorkStreamActivityMappingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCostByResourceById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetByIdWorkStreamActivityQuery(id);
            var costByResource = await _sender.Send(query, cancellationToken);
            return Ok(costByResource);
        }




    }
}