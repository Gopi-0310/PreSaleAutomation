using AutoMapper;
using FluentValidation.Validators;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Commands.WorkStreamCommands;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Commands.WorkStreamActivityCommands
{
    public sealed record CreateWorkStreamActivityCommand(string Activity, Guid RoleId, Guid WorkStreamActivityId, double Hours,double Week, string? Description, Guid? ParentId) : ICommand<WorkStreamActivityResponse>;
    public sealed record CreateWorkStreamActivityRequest(string Activity, Guid RoleId, Guid WorkStreamActivityId, double Hours,double Week, string? Description, Guid? ParentId = null);
    public class CreateWorkSreamActivityHandler : ICommandHandler<CreateWorkStreamActivityCommand, WorkStreamActivityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkStreamActivityRepository _WorkSream;

        public CreateWorkSreamActivityHandler(IMapper mapper, IWorkStreamActivityRepository workStream)
        {
            _mapper = mapper;
            _WorkSream = workStream;
        }

        public async Task<WorkStreamActivityResponse> Handle(CreateWorkStreamActivityCommand command, CancellationToken cancellationToken)
        {
            WorkStreamActivity WorkStreamActivity = new();

            // Map command to entity For New Create
            var item = _mapper.Map<WorkStreamActivity>(command);
            item.CreatedBy = Guid.NewGuid();// Replace with the appropriate user ID
            //if(item.ChildId == Guid.Empty)
            ///{
             //   item.ChildId = null;
            //}

            WorkStreamActivity = await _WorkSream.AddAsync(item);

            return _mapper.Map<WorkStreamActivityResponse>(WorkStreamActivity);


        }
    }
}
