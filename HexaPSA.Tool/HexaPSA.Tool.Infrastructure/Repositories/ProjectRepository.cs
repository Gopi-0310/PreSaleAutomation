using Dapper;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using System.Data;
using System.Xml.Linq;

namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DapperDataContext _context;
        public ProjectRepository(DapperDataContext context)
        {
            _context = context;
        }

        private DataTable GetProjectUserRoleMappings(Project project)
        {
            DataTable table = new DataTable();

            table.Columns.Add(new DataColumn("ProjectId"));
            table.Columns.Add(new DataColumn("UserId"));
            table.Columns.Add(new DataColumn("RoleId"));
            table.Columns.Add(new DataColumn("CurrentStatus"));

          
            foreach (ProjectUserEstimationMapping item in project.ProjectUserEstimationMappings)
            {
                DataRow dr = table.NewRow();
                dr["ProjectId"] = Guid.Empty;
                dr["UserId"] = item.UserId;
                dr["RoleId"] = item.RoleId;
                dr["CurrentStatus"] = item.CurrentStatus;

                table.Rows.Add(dr);
            }

            return table;
        }

        private DataTable GetProjectTechnologyMappings(Project project)
        {
            DataTable table = new DataTable();

            table.Columns.Add(new DataColumn("ProjectId"));
            table.Columns.Add(new DataColumn("TechnologyId"));
            table.Columns.Add(new DataColumn("CurrentStatus"));

            foreach (ProjectTechMapping item in project.ProjectTechMappings)
            {
                DataRow dr = table.NewRow();
                dr["ProjectId"] = Guid.Empty;
                dr["TechnologyId"] = item.Id;
                dr["CurrentStatus"] = item.CurrentStatus;
                table.Rows.Add(dr);
            }
            return table;
        }
        public async Task<int> SaveAsync(Project project)
        {
            var spQry = "uspSaveProject";

            Task<int> returnResult;

            DataTable projectUserMapping = GetProjectUserRoleMappings(project);
            DataTable projectTechnologyMapping = GetProjectTechnologyMappings(project);
            using (var connection = _context.CreateConnection())
            {
                
                return await connection.ExecuteAsync(spQry,
                new
                {
                    Name = project.Name,
                    ProjectTypeId = project.ProjectTypeId,
                    ProjectId = project.Id,
                    EffectiveStartDate = project.EffectiveStartDate,
                    EffectiveEndDate = project.EffectiveEndDate,
                    UserId = project.CreatedBy,
                    ProjectUserRoleList = projectUserMapping.AsTableValuedParameter("ProjectUserRoleMapping"),
                    ProjectTechnologyMapping = projectTechnologyMapping.AsTableValuedParameter("ProjectTechnologyMapping")
                    
                }, 
                    commandType: CommandType.StoredProcedure);

                //return await connection.ExecuteAsync(spQry, parameters);
            }
        }
            public async Task<Project> AddAsync(Project project)
        {
            var query = @"
        INSERT INTO Project (Name, ProjectTypeId, EffectiveStartDate, EffectiveEndDate)
        OUTPUT inserted.Id
        VALUES (@Name, @ProjectTypeId, @EffectiveStartDate, @EffectiveEndDate)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", project.Name, DbType.String);
            parameters.Add("ProjectTypeId", project.ProjectTypeId, DbType.Guid);
            parameters.Add("EffectiveStartDate", project.EffectiveStartDate, DbType.DateTime);
            parameters.Add("EffectiveEndDate", project.EffectiveEndDate, DbType.DateTime);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
            parameters.Add("CreatedBy", project.CreatedBy, DbType.Guid);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, parameters);

                var addedProject = new Project
                {
                    Id = id,
                    Name = project.Name,
                    ProjectTypeId = project.ProjectTypeId,
                    EffectiveStartDate = project.EffectiveStartDate,
                    EffectiveEndDate = project.EffectiveEndDate
                };

                return addedProject;
            }
        }

        public async Task<IEnumerable<ProjectResponseAll>> GetAllAsync()
        {
            var query = @"
        SELECT
            p.Id,
            p.Name,
            p.ProjectTypeId,
            pt.Id,
            pt.Name,
            p.EffectiveStartDate,
            p.EffectiveEndDate
        FROM Project p 
        INNER JOIN ProjectType pt ON p.ProjectTypeId = pt.Id
         WHERE p.IsDestroyed = 0";

            using (var connection = _context.CreateConnection())
            {
                var projectDictionary = new Dictionary<Guid, ProjectResponseAll>();

                var projects = await connection.QueryAsync<ProjectResponseAll, ProjectTypeResponse, ProjectResponseAll>(
                    query,
                    (proj, protyperes) =>
                    {
                        ProjectResponseAll projectEntry;
                        if (!projectDictionary.TryGetValue(proj.Id, out projectEntry))
                        {
                            projectEntry = proj;

                            projectEntry.ProjectTypes = new List<ProjectTypeResponse>();
                            projectDictionary.Add(proj.Id, projectEntry);
                        }

                        if (protyperes != null)
                        {
                            projectEntry.ProjectTypes.Add(protyperes);
                        }

                        return projectEntry;
                    },
                    splitOn: "Id,Id"
                );

                return projects;
            }
        }
        public Task<Project> UpdateAsync(Project item)
        {
            throw new NotImplementedException();
        }

        public async Task<ProjectResponse> GetProjectByIdAsync(Guid id)
        {
            var query = @"
        SELECT
            P.Id ,
            P.Name ,
            P.EffectiveStartDate ,
            P.EffectiveEndDate ,
            P.ProjectTypeId,
            PT.Id ,
            PT.Name ,
            PUEM.ProjectId,
            U.Id ,
            U.FullName ,
            R.Id ,
            R.Name ,
            TEC.Id ,
            TEC.Name
            FROM Project AS P
            LEFT JOIN ProjectType AS PT ON P.ProjectTypeId = PT.Id
            LEFT JOIN ProjectUserEstimationMapping AS PUEM ON P.Id = PUEM.ProjectId  
            LEFT JOIN Role AS R ON PUEM.RoleId = R.Id
            LEFT JOIN [User] AS U ON PUEM.UserId = U.Id
            LEFT JOIN ProjectTechMapping AS PTM ON PTM.ProjectId = P.Id
            LEFT JOIN Technology AS TEC ON TEC.Id = PTM.TechnologyId
            WHERE P.Id = @ProjectId";

            using (var connection = _context.CreateConnection())
            {
                var projectDictionary = new Dictionary<Guid, ProjectResponse>();
                Dictionary<string, string> checkUserExist = new Dictionary<string, string>();


                var project = await connection.QueryAsync < ProjectResponse, ProjectTypeResponse, UserResponse,RoleProjectResponse,  TechnologyResponse, ProjectResponse>(
                    query,
                    (proj, protyperes,userResp,roleResp, tech) =>
                    {
                        ProjectResponse projectEntry;
                        if (!projectDictionary.TryGetValue(proj.Id, out projectEntry))
                        {
                            projectEntry = proj;
                            
                            projectEntry.ProjectTypes   = new List<ProjectTypeResponse>();
                            projectEntry.EstimatedUsers = new List<UserRoleResponse>();
                            projectEntry.Technology = new List<TechnologyResponse>();
                            projectDictionary.Add(proj.Id, projectEntry);
                        }

                        if (userResp != null  && roleResp!=null)
                        {
                            var UserRoleREesp=new UserRoleResponse();
                            UserRoleREesp.User = userResp;
                            UserRoleREesp.Role = roleResp;
                            string UserId = UserRoleREesp.User.Id.ToString();
                            if (!checkUserExist.ContainsValue(UserId))
                            {
                                projectEntry.EstimatedUsers.Add(UserRoleREesp);
                                checkUserExist.Add(UserId, UserId);
                            }                  
                        }
                        if (tech != null)
                        {
                            if (!projectEntry.Technology.Any(t => t.Id == tech.Id))
                            {
                                projectEntry.Technology.Add(tech);
                            }
                        }

                        if (protyperes != null)
                        {
                            if (!projectEntry.ProjectTypes.Any(pt => pt.Id == protyperes.Id))
                            {
                                projectEntry.ProjectTypes.Add(protyperes);
                            }
                        }
                        return projectEntry;
                    },
                    new { ProjectId = id },
                    splitOn: "Id,ProjectTypeId,ProjectId,Id,Id");
                return project?.FirstOrDefault();
            }
        }

        public async Task<Project> DeleteAsync(Guid id)
        {
            var query = "UPDATE Project SET IsDestroyed = 1 WHERE Id = @id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
                var deletedProject = new Project { Id = id };
                return deletedProject;
            }
        }
       
        public async Task<Project> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM Project WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Project>(query, new { Id = id });
            }
        }


        public Task<Project> GetAllAsync(string name)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Project>> IReadRepository<Project, Guid>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<int>> WeeksByProject(Guid projectId)
        {

            var query = @"SELECT
                            distinct
                            Week
                            FROM WorkStreamActivity WSA
                            INNER JOIN WorkStream WS on WSA.WorkStreamActivityId = WS.Id
                            WHERE WS.ProjectId = @Id
                            ORDER BY Week ASC";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<int>(query, new { Id = projectId });
            }
        }
        public async Task<IEnumerable<WorkStreamActivityResp>> GetWorkStreamActivities(Guid projectId)
        {
            var spQry = @"
                            WITH WorkStreamWithParent 
                              ( Name,ID,Activity,Role,RoleDescription,WorkStreamActivityId,Hours,Week,ParentId,
                              ParentActivity,ParentActivityId,Rate,
                              depth)
                            AS

                            (

                            SELECT  W.Name,WS.ID,WS.Activity,R.Code,R.Name,WS.WorkStreamActivityId,WS.Hours,WS.Week,WS.ParentId,WS.Activity,WS.Id,RR.Rate,
                                    0 
                            FROM    WorkStreamActivity WS
                            INNER JOIN WorkStream W on WS.WorkStreamActivityId=W.Id
                            LEFT JOIN Role R ON R.Id=WS.RoleId
                             LEFT JOIN ResourceRate RR ON RR.ProjectId=W.ProjectId and RR.RoleId=WS.RoleId
                            WHERE   ParentId IS NULL and W.ProjectId=@ProjectId

                            UNION ALL 

                            SELECT   W.Name,WS.ID,WS.Activity,R.Code,R.Name,WS.WorkStreamActivityId,WS.Hours,WS.Week,WS.ParentId,
                                     WSP.Activity,WS.ParentId,RR.Rate,
                                     WSP.depth + 1
                            FROM     WorkStreamActivity WS
                             JOIN WorkStream W on WS.WorkStreamActivityId=W.Id
                             JOIN Role R ON R.Id=WS.RoleId
                            JOIN ResourceRate RR ON RR.ProjectId=W.ProjectId and RR.RoleId=WS.RoleId
                               JOIN  WorkStreamWithParent WSP ON WS.ParentId = WSP.ID WHERE W.ProjectId=@ProjectId
  
                            )

                            SELECT   *
                            FROM     WorkStreamWithParent
                            ORDER BY WorkStreamActivityId";
            using (var connection = _context.CreateConnection())
            {

                return await connection.QueryAsync<WorkStreamActivityResp>(spQry,
                new
                {
                    ProjectId = projectId
                },
                    commandType: CommandType.Text);

            }
        }
        public async Task<IEnumerable<CostByResourceResp>> GetCostByResource(Guid projectId)
        {

            var qry = @"SELECT Code,Util.RoleId,Util.Name as Role,Util.Hours,Util.Rate,Streams.TotalHours,(Streams.TotalHours/Util.Hours) as Days,(Streams.TotalHours*Util.Rate)  As Fees FROM
                         (
                        (select CU.Hours,RR.Rate,CU.RoleId,R.Code,R.Name from CapacityUtilization CU 
                        INNER JOIN ResourceRate RR ON CU.ProjectId=RR.ProjectId and CU.RoleId =RR.RoleId
                        INNER JOIN Role R ON R.Id=CU.RoleId WHERE CU.ProjectId=@Id)  Util
                        INNER JOIN (
                        SELECT WSA.RoleID,SUM(WSA.Hours) as TotalHours FROM WorkStream WS 
                        INNER JOIN WorkStreamActivity WSA ON WSA.WorkStreamActivityId=WS.Id
                        WHERE Hours is not null and WS.ProjectID=@Id
                        group by WSA.RoleId) Streams  ON Streams.RoleId=Util.RoleId
                        ) ";
            using (var connection = _context.CreateConnection())
            {
                 
                return await connection.QueryAsync<CostByResourceResp>(qry,
                new
                {
                    Id = projectId
                }
                   );

            }


        }

        public async Task<IEnumerable<CostByWorkStream>> GetCostByWorkStreams(Guid projectId) {

            var qry = @"SELECT W.Name as Thread,SUM(WSA.HOurs*RR.Rate) Fees,SUM(WSA.HOurs) Hours  FROM WorkStream W 
INNER JOIN WorkStreamActivity WSA ON WSA.WorkStreamActivityId=W.Id
INNER JOIN ResourceRate RR ON RR.ProjectId=W.ProjectId  and RR.RoleId=WSA.RoleId
WHERE WSA.Hours is not null
and W.ProjectId=@Id
group by W.Name ";
            using (var connection = _context.CreateConnection())
            {

                return await connection.QueryAsync<CostByWorkStream>(qry,
                new
                {
                    Id = projectId
                },
                    commandType: CommandType.Text);

            }


        }
    }
}
