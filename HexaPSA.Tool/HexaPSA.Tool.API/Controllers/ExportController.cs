using AutoMapper;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Queries.ExportQueries;
using HexaPSA.Tool.Export;
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
    public sealed class ExportController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private IExport _export;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        public ExportController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }
        /// <summary>
        /// Export to LOE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("LOE/{id:guid}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LOE(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetLOEExportById(id);

            var project = await _sender.Send(query, cancellationToken);

            _export = new LOE(project);
            var stream = _export.Execute();
            //insert to activity log projectId,Export,CreatedDate
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LOE.xlsx");
        }


        /// <summary>
        /// Export to LOE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Total CostByResource weeks and hours and cost</returns>
        [HttpGet("CostByResource/{id:guid}")]
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CostByResource(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetLOEExportById(id);

            var project = await _sender.Send(query, cancellationToken);
            //insert to activity log projectId,Export,CreatedDate
            return Ok(project);
        }
    }
}