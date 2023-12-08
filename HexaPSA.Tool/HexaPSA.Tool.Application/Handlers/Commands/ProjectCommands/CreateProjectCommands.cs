using AutoMapper;
using HexaPSA.Tool.Application.Abstractions.Messaging;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Handlers.Queries.ProjectQuery;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Handlers.Commands.ProjectCommands
{
    public class CreateProjectCommand : ICommand<ProjectResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectTypeId { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public List<UserRoleRequest> EstimatedUsers { get; set; }
        public List<TechnologyRequest> Technologies { get; set; }

    }
    public class CreateProjectRequest  
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectTypeId { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public List<UserRoleRequest> EstimatedUsers { get; set; }
        public List<TechnologyRequest> Technologies { get; set; }

    }
    public class UserRoleRequest
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }

    public class TechnologyRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class CreateProjectHandler : ICommandHandler<CreateProjectCommand, ProjectResponse>
    {
        private readonly ISender _sender;

        private readonly IMapper _mapper;

        private readonly IProjectRepository _projectRepository;
        

        public CreateProjectHandler(ISender sender, IMapper mapper, IProjectRepository projectRepository, IUserRepository userRepository, ITechnologyRepository technologyRepository)
        {
            _sender = sender;
            _mapper = mapper;
            _projectRepository = projectRepository;
            
        }

        private List<ProjectUserEstimationMapping> PrepareProjectUserEstimationMappingList(CreateProjectCommand command, ProjectResponse proj)
        {
            var nonChangedEstimatedUserList = new List<ProjectUserEstimationMapping>();
            var removedEstimatedUserList = new List<ProjectUserEstimationMapping>();
            var addedEstimatedUserList = new List<ProjectUserEstimationMapping>();
            var projectUserEstimationMappingList = new List<ProjectUserEstimationMapping>();

            foreach (var eItem in command.EstimatedUsers)
            {
                var isMatchingUserAndRole = false;

                foreach (var item in proj.EstimatedUsers)
                {
                    if (eItem.UserId == item.User.Id && eItem.RoleId == item.Role.Id)
                    {
                        isMatchingUserAndRole = true;
                        nonChangedEstimatedUserList.Add(new ProjectUserEstimationMapping()
                        {
                            UserId = eItem.UserId,
                            RoleId = eItem.RoleId
                        });
                        break;
                    }
                }

                if (!isMatchingUserAndRole)
                {
                    addedEstimatedUserList.Add(new ProjectUserEstimationMapping()
                    {
                        UserId = eItem.UserId,
                        RoleId = eItem.RoleId,
                        CurrentStatus = "Insert"
                    });
                }
            }

            foreach (var item in proj.EstimatedUsers)
            {
                var isStillEstimated = false;

                foreach (var item2 in nonChangedEstimatedUserList)
                {
                    if (item2.UserId == item.User.Id && item2.RoleId == item.Role.Id)
                    {
                        isStillEstimated = true;
                        break;
                    }
                }

                if (!isStillEstimated)
                {
                    removedEstimatedUserList.Add(new ProjectUserEstimationMapping()
                    {
                        UserId = item.User.Id,
                        RoleId = item.Role.Id,
                        CurrentStatus = "Delete"
                    });
                }
            }

            projectUserEstimationMappingList.AddRange(addedEstimatedUserList);
            projectUserEstimationMappingList.AddRange(removedEstimatedUserList);

            return projectUserEstimationMappingList;
        }

        private List<ProjectTechMapping> PrepareTechnologyList(CreateProjectCommand command, ProjectResponse proj)
        {
            var nonChangedTechnologyList = new List<ProjectTechMapping>();
            var removedTechnologyList = new List<ProjectTechMapping>();
            var addedTechnologyList = new List<ProjectTechMapping>();
            var projectTechMappingList = new List<ProjectTechMapping>();
            foreach (var eItem in command.Technologies)//2,3
            {
                foreach (var item in proj.Technology)//1,3
                {
                    if (eItem.Id!="" && new Guid (eItem.Id) == item.Id)
                    {
                        nonChangedTechnologyList.Add(new ProjectTechMapping() { Id= item.Id, ProjectId= proj.Id});
                    }
                }
            }

            foreach (var item in proj.Technology)//1,2
            {
                foreach (var item2 in nonChangedTechnologyList)//2
                {
                    if (item2.Id != item.Id)
                    {
                        removedTechnologyList.Add(new ProjectTechMapping() { Id= item.Id, ProjectId = proj.Id });
                    }
                }
            }

            foreach (var eItem in command.Technologies)//2,3
            {
                foreach (var item in nonChangedTechnologyList)//2
                {
                    if (eItem.Id != "" && new Guid(eItem.Id) != item.Id)
                    {
                        addedTechnologyList.Add(new ProjectTechMapping() { Id = item.Id, ProjectId = proj.Id });

                    }
                   
                }
            }

            addedTechnologyList.ForEach(s =>
            {
                projectTechMappingList.Add(new ProjectTechMapping()
                {
                   Id=s.Id,
                   ProjectId=s.ProjectId,
                    CurrentStatus = "Insert"
                });
            });

            removedTechnologyList.ForEach(s =>
            {
                projectTechMappingList.Add(new ProjectTechMapping()
                {
                    Id = s.Id,
                    ProjectId = s.ProjectId,
                    CurrentStatus = "Delete"
                });
            });
            return projectTechMappingList;
        }
        public async Task<ProjectResponse> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
        {

            if (command.Id != Guid.Empty)
            {
                var query = new GetProjectQuery(command.Id);

                var proj = await _sender.Send(query, cancellationToken);

                // Map command to entity
                var existingProject = new Project
                {
                    Name = command.Name,
                    ProjectTypeId = command.ProjectTypeId,
                    EffectiveStartDate = command.EffectiveStartDate,
                    EffectiveEndDate = command.EffectiveEndDate,
                    Id=command.Id
                };
                existingProject.ProjectUserEstimationMappings = PrepareProjectUserEstimationMappingList(command, proj);

                existingProject.ProjectTechMappings = PrepareTechnologyList(command, proj);

                await _projectRepository.SaveAsync(existingProject);

                return _mapper.Map<ProjectResponse>(existingProject);
            }
            else
            {
                // Map command to entity
                var newProject = new Project
                {
                    Name = command.Name,
                    ProjectTypeId = command.ProjectTypeId,
                    EffectiveStartDate = command.EffectiveStartDate,
                    EffectiveEndDate = command.EffectiveEndDate
                };
                newProject.ProjectUserEstimationMappings = new List<ProjectUserEstimationMapping>();
                command.EstimatedUsers.ForEach(s =>
                {
                    newProject.ProjectUserEstimationMappings.Add(new ProjectUserEstimationMapping()
                    {
                        UserId = s.UserId,
                        RoleId = s.RoleId,
                        CurrentStatus="Insert"
                        
                    });
                });

               newProject.ProjectTechMappings = new List<ProjectTechMapping>();
                command.Technologies.ForEach(s =>
                {
                    newProject.ProjectTechMappings.Add(new ProjectTechMapping()
                    {
                        Id = new Guid(s.Id),
                        CurrentStatus = "Insert"

                    });
                });

                 await _projectRepository.SaveAsync(newProject);

                return _mapper.Map<ProjectResponse>(newProject);

            }
        }
    }
}
