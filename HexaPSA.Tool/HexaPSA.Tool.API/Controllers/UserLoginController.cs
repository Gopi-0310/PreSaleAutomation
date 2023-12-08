using AutoMapper;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using static CreateUserLoginHandler;

namespace HexaPSA.Tool.API.Controllers
{
    ///<Summary>
    /// UserLoginController
    ///</Summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class UserLoginController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLoginController"/> class.
        /// </summary>
        /// <param name="sender"></param>
        public UserLoginController(ISender sender, IMapper mapper, IEmailService emailService)
        {
            _sender = sender;
            _mapper = mapper;
            _emailService = emailService;
        }

        /// <summary>
        /// Creates a new  User Login based on the specified request.
        /// </summary>
        /// <param name="request">The create User Login request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created User Login.</returns>

        [HttpPost]
        [ProducesResponseType(typeof(AllUserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUserLogin([FromBody] CreateUserLoginRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateUserLoginCommand>(request);
            var createdUser = await _sender.Send(command, cancellationToken);

            _emailService.SendEmail(request.EMail, Application.Utilities.Constants.AccountConfirmation, "Body");

            return CreatedAtAction(nameof(GetUserLoginById), new { id = createdUser.Id }, createdUser);
        }


        /// <summary>
        /// Gets the  User Login details with the specified identifier, if it exists.
        /// </summary>
        /// <param name="id">The capacity identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The capacityUtilization with the specified identifier, if it exists.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AllUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserLoginById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetUserLoginByIdQuery(id);
            var user = await _sender.Send(query, cancellationToken);


            return Ok(user);
        }
    }
}
