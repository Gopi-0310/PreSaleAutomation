using AutoMapper;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Contracts.Technology;
using HexaPSA.Tool.Application.Handlers.Commands.CapacityUtilizationsCommands;
using HexaPSA.Tool.Application.Handlers.Commands.CreateProjectTypeCommands;
using HexaPSA.Tool.Application.Handlers.Commands.ProjectCommands;
using HexaPSA.Tool.Application.Handlers.Commands.ResourceRateCommands;
using HexaPSA.Tool.Application.Handlers.Commands.RoleCommands;
using HexaPSA.Tool.Application.Handlers.Commands.TeamConfigurationCommands;
using HexaPSA.Tool.Application.Handlers.Commands.WorkStreamActivityCommands;
using HexaPSA.Tool.Application.Handlers.Commands.WorkStreamCommands;
using HexaPSA.Tool.Application.Handlers.Queries.ActivityLogQueries;
using HexaPSA.Tool.Application.Handlers.Queries.CapacityUtilizationQueries;
using HexaPSA.Tool.Application.Handlers.Queries.ProjectQuery;
using HexaPSA.Tool.Application.Handlers.Queries.TeamConfigurationQueries;
using HexaPSA.Tool.Application.Handlers.Queries.WorkStreamActivityQueries;
using HexaPSA.Tool.Domain.Entities;
using static CreatePresalesTimeTrackerHandler;
using static CreateTechnologyHandler;
using static CreateUserHandler;
using static CreateUserLoginHandler;

namespace HexaPSA.Tool.Application.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectTypeResponse, ProjectType>().ReverseMap();
            CreateMap<CreateProjectTypeCommand, ProjectType>().ReverseMap();
            CreateMap<CreateProjectTypeRequest, CreateProjectTypeCommand>().ReverseMap();

            CreateMap<Project, ProjectResponse>().ReverseMap();
            CreateMap<ProjectResponse, ProjectResponse>().ReverseMap();
            CreateMap<ProjectResponse, ProjectResponse>().ReverseMap();
            CreateMap<UserRoleResponse, UserRoleResponse>().ReverseMap();
            CreateMap<CreateProjectRequest, CreateProjectCommand>().ReverseMap();
            CreateMap<Project, ProjectResponse>().ReverseMap();

            CreateMap<ChartResponse, ChartResponse>().ReverseMap();
            CreateMap<GetChartAllQuery, ChartResponse>().ReverseMap();


            CreateMap<TechnologyResponse, TechnologyResponse>().ReverseMap();
            CreateMap<TechnologyDropResponse, Technology>().ReverseMap();
            CreateMap<Technology,TechnologyDropResponse>().ReverseMap();
            CreateMap<CreateTechnologyHandler, Technology>().ReverseMap();
            CreateMap<CreateTechnologyRequest, CreateTechnologyCommand>().ReverseMap();
            CreateMap<CreateTechnologyCommand, Technology>().ReverseMap();

            CreateMap<WorkStreamChartsResponse, WorkStreamChartsMappingResponse>();

            CreateMap<WorkStreamActivityWeeksResponse, WorkStreamActivityWeeksResponse>().ReverseMap();

            CreateMap<WorkStreamChartDetails, WorkStreamChartDetails>().ReverseMap();

            CreateMap<WorkStreamUpdateResponse, WorkStreamActivity>().ReverseMap();
            CreateMap<UpdateWorkStreamActivityCommand, WorkStreamActivity>().ReverseMap();
            CreateMap<UpdateWorkStreamActivityRequest, UpdateWorkStreamActivityCommand>().ReverseMap();

            CreateMap<ResourceRateCardResponse, ResourceRate>().ReverseMap();
            CreateMap<ResourceRateCardResponseRoleId, ResourceRate>().ReverseMap();
            CreateMap<CreateResourceRateCardCommand, ResourceRate>().ReverseMap();
            CreateMap<CreateResourceRateCardRequest, CreateResourceRateCardCommand>().ReverseMap();

            CreateMap<CapacityUtilizationResponse, CapacityUtilization>().ReverseMap();
            CreateMap<CreateCapacityUtilizationCommand, CapacityUtilization>().ReverseMap();
            CreateMap<CreateCapacityUtilizationRequest, CreateCapacityUtilizationCommand>().ReverseMap();

            CreateMap<CapacityMappingResponse, CapacityMappingResponse>().ReverseMap();
            CreateMap<GetCapacityUtilizationQuery, CapacityMappingResponse>().ReverseMap();
           
            CreateMap<CapacityRoleResponse, Role>().ReverseMap();

            CreateMap<WorkStreamActivityMappingResponse, WorkStreamActivityMappingResponse>().ReverseMap();
            CreateMap<GetByIdWorkStreamActivityQuery, WorkStreamActivityMappingResponse>().ReverseMap();

            CreateMap<GanttChartResponse, Project>().ReverseMap();

            CreateMap<WorkStreamTotalResponse, WorkStreamTotalResponse>().ReverseMap();


            CreateMap<RoleResponse, Role>().ReverseMap();
            CreateMap<CreateRoleCommand, Role>().ReverseMap();
            CreateMap<CreateRoleRequest, CreateRoleCommand>().ReverseMap();

            CreateMap<WorkStreamResponse, WorkStream>().ReverseMap();
            CreateMap<CreateWorkStreamCommands, WorkStream>().ReverseMap();
            CreateMap<CreateWorkStreamRequest, CreateWorkStreamCommands>().ReverseMap();

            CreateMap<WorkStreamResponse, WorkStream>().ReverseMap();
            CreateMap<UpdateWorkStreamCommands, WorkStream>().ReverseMap();
            CreateMap<UpdateWorkStreamRequest, UpdateWorkStreamCommands>().ReverseMap();


            CreateMap<WorkStreamActivityResponse, WorkStreamActivity>().ReverseMap();
            CreateMap<CreateWorkStreamActivityCommand, WorkStreamActivity>().ReverseMap();
            CreateMap<CreateWorkStreamActivityRequest, CreateWorkStreamActivityCommand>().ReverseMap();

            CreateMap<PresalesTimeTrackerResponse, PresalesTimeTracker>().ReverseMap();
            CreateMap<CreatePresalesTimeTrackerCommand, PresalesTimeTracker>().ReverseMap();
            CreateMap<CreatePresalesTimeTrackerRequest, CreatePresalesTimeTrackerCommand>().ReverseMap();
            CreateMap<UserResponsep, User>().ReverseMap();
            CreateMap<ProjectResponsepst, Project>().ReverseMap();


            CreateMap<User, AllUserResponse>().ReverseMap();
            CreateMap<CreateUserCommand, User>().ReverseMap();
            CreateMap<CreateUserRequest, CreateUserCommand>().ReverseMap();

            CreateMap<UserLogin, AllUserLoginResponse>().ReverseMap();
            CreateMap<CreateUserLoginCommand, UserLogin>().ReverseMap();
            CreateMap<CreateUserLoginRequest, CreateUserLoginCommand>().ReverseMap();

            //TeamConfigration
            CreateMap<TeamConfigResponse, TeamConfigResponse>().ReverseMap();
            CreateMap<TeamConfigurationResponse, TeamConfigurationResponse>().ReverseMap();
            CreateMap<CreateTeamConfigurationsCommand, TeamConfigResponse>().ReverseMap();
            CreateMap<CreateTeamConfigurationsRequest, CreateTeamConfigurationsCommand>().ReverseMap();

            //ActivityLog
            CreateMap<GetRecentExportAcitivityQuery, RecentExportAcivitiesResponse>().ReverseMap();
            CreateMap<GetRecentAcitivityQuery, RecentAcivitiesResponse>().ReverseMap();

        }
    }
}
