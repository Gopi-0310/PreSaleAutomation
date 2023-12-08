using AutoMapper;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Commands.WorkStreamActivityCommands;
using HexaPSA.Tool.Application.Handlers.Commands.WorkStreamCommands;
using HexaPSA.Tool.Application.Handlers.Queries.WorkStreamActivityQueries;
using HexaPSA.Tool.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.API.Controllers
{
    /// <summary>
    /// Workstream controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class WorkStreamActivityController : ControllerBase
    {
        private readonly ISender _iSender;
        private readonly IMapper _iMapper;
        private int accumulatedWeeks = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkStreamActivityController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mapper"></param>
        public WorkStreamActivityController(ISender sender, IMapper mapper)
        {
            _iSender = sender;
            _iMapper = mapper;

        }

        /// <summary>
        /// Creates a new  Workstream based on the specified request.
        /// </summary>
        /// <param name="request">The create  Workstream request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created  Workstream.</returns>

        [HttpPost]
        [ProducesResponseType(typeof(WorkStreamActivityResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateWorkStreamActivity([FromBody] CreateWorkStreamActivityRequest request, CancellationToken cancellationToken)
        {
            var createCommand = _iMapper.Map<CreateWorkStreamActivityCommand>(request);

            var CreateWorkStream = await _iSender.Send(createCommand, cancellationToken);

            return Ok(CreateWorkStream);

        }


        /// <summary>
        /// Gets all of the  WorkstreamActivity.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of WorkstreamActivity Details.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<WorkStreamActivityResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllWorkStreamActivity(CancellationToken cancellationToken)
        {
            var query = new GetAllWorkStreamActivityQuery();

            var workStream = await _iSender.Send(query, cancellationToken);

            return Ok(workStream);
        }


        /// <summary>
        /// Gets the WorkstreamActivity with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The Workstream identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The WorkstreamActivity with the specified identifier</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(WorkStreamActivityMappingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWorkStreamActivityById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetByIdWorkStreamActivityQuery(id);
            var workStreamActivity = await _iSender.Send(query, cancellationToken);
            return Ok(workStreamActivity);
        }



        /// <summary>
        /// Gets the WorkstreamActivity with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The Workstream identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The WorkstreamActivity with the specified identifier</returns>
        /*[HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(WorkStreamActivityMappingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWorkStreamActivityById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetByIdWorkStreamActivityQuery(id);
            var workStreamActivity = await _iSender.Send(query, cancellationToken);

            // Create a dictionary to easily lookup items by their Id
            *//*var itemDictionary = workStreamActivity.ToDictionary(item => item.Id);

            // Initialize the root list for building the tree
            mappingResponse.Children = new List<WorkStreamActivityMappingResponse>();
           

            // Iterate through the items to build the tree
            foreach (var item in workStreamActivity)
            {
                if (item.ParentId == item.Id || item.ParentId == null)
                {
                    mappingResponse.Children.Add(item);
                }
                else
                {
                    
                }
            }*//*

            return Ok(workStreamActivity);
        }*/




        /// <summary>
        /// Deletes a WorkstreamActivity record with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the Workstream to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>No content if the WorkstreamActivity was successfully deleted, or a not found response if the record does not exist.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWorkStreamActivity(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteWorkStreamActivityCommand(id);

            await _iSender.Send(command, cancellationToken);

            return NoContent();
        }


        /// <summary>
        /// Creates a new WorkstreamActivity record based on the specified request.
        /// </summary>
        /// <param name="id">The ID of the workstreamActivity to update.</param>
        /// <param name="request">The create Workstream request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created WorkstreamActivity record.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateWorkStream(Guid id, [FromBody] UpdateWorkStreamActivityRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateWorkStreamActivityCommand(id,request.Activity, request.RoleId, request.WorkStreamActivityId, request.Hours, request.Week, request.Description, request.ParentId);

            await _iSender.Send(command, cancellationToken);

            return NoContent();
        }






        /// <summary>
        ///Get The WorkStream Data using fo displaying Charts
        /// </summary>
        /// <param name="id">The Workstream identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The WorkstreamWeeks datas with the specified identifier</returns>
        [HttpGet("GetChartDetailsById/{id:guid}")]
        [ProducesResponseType(typeof(WorkStreamChartDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWorkStreamWeeksById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetByIdWorkStreamWeeks(id);
            var workStreamActivity = await _iSender.Send(query, cancellationToken);
            return Ok(workStreamActivity);
        }
    }
    
}
