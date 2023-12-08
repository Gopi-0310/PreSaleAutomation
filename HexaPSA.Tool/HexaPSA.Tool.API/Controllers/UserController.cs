using AutoMapper;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static CreateUserHandler;

namespace HexaPSA.Tool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class UserController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public UserController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AllUserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var query = new GetAllUsersQuery();
            var users = await _sender.Send(query, cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AllUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _sender.Send(query, cancellationToken);
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AllUserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateUserCommand>(request);
            var createdUser = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

    }
}
