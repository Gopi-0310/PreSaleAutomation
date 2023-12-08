
using AutoMapper;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Queries.ActivityLogQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexaPSA.Tool.API.Controllers
{
    /// <summary>
    /// ActivityLog
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ActivityLogController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityLogController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        ///  /// <param name="mapper"></param>
        public ActivityLogController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all of Recent Export Acivities
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection Recent Export Acivities Data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<RecentExportAcivitiesResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecentExportAcitivity(CancellationToken cancellationToken)
        {
            var query = new GetRecentExportAcitivityQuery();
            var recentAcivities = await _sender.Send(query, cancellationToken);
            return Ok(recentAcivities);
        }

        /// <summary>
        /// Gets all of Recent Acivities
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection Recent Acivities Data.</returns>
        [HttpGet("GetRecentAcitivity")]
        [ProducesResponseType(typeof(List<RecentAcivitiesResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecentAcitivity(CancellationToken cancellationToken)
        {
            var query = new GetRecentAcitivityQuery();
            var recentAcivities = await _sender.Send(query, cancellationToken);
            return Ok(recentAcivities);
        }
    }
}
