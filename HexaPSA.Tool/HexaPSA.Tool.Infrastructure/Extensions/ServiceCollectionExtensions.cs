using Microsoft.Extensions.DependencyInjection;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Infrastructure.DataContexts;
using HexaPSA.Tool.Infrastructure.Repositories;

namespace HexaPSA.Tool.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddSingleton<DapperDataContext>();
            services.AddScoped<IProjectTypeRepository, ProjectTypeRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPresalesTimeTrackerRepository, PresalesTimeTrackerRepository>();

            services.AddScoped<ITechnologyRepository, TechnologyRepository>();

            services.AddScoped<ICapacityUtilizationRepository, CapacityUtilizationRepository>();
            services.AddScoped<IResourceRateCardRepository, ResourceRateCardRepository>();
            services.AddScoped<IWorkStreamRepository, WorkStreamRepository>();
            services.AddScoped<IProjectRepository,ProjectRepository>();
            services.AddScoped<IWorkStreamActivityRepository, WorkStreamActivityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChartRepository, ChartRepository>();
            services.AddScoped<IGanttChartRepository, GanttChartRepository>();
            services.AddScoped<ITeamConfigurationRepository, TeamConfigurationRepository>();
            services.AddScoped<IUserLoginRepository, UserLoginRepository>();
            services.AddScoped<IActivityLogRepository, ActivityLogRepository>();

        }

    }
}
