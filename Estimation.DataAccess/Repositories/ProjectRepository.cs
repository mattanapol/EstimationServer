using Estimation.DataAccess.Models;
using Estimation.Domain.Models;
using Estimation.Services.Interfaces;
using Estimation.Services.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.DataAccess.Repositories
{
    public class ProjectRepository : BaseProjectRepository, IProjectRepository
    {
        /// <summary>
        /// Project repository
        /// </summary>
        /// <param name="projectDbContext"></param>
        /// <param name="typeMappingService"></param>
        public ProjectRepository(ProjectDbContext projectDbContext,
                                  ITypeMappingService typeMappingService)
            : base(projectDbContext, typeMappingService)
        {
        }

        public async Task<ProjectInfo> CreateProjectInfo(ProjectInfo projectInfo)
        {
            // Add main material record
            var projectDb = TypeMappingService.Map<ProjectInfo, ProjectInfoDb>(projectInfo);

            DbContext.ProjectInfo.Add(projectDb);

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<ProjectInfoDb, ProjectInfo>(projectDb);
        }

        public Task<IEnumerable<ProjectInfo>> GetAllProjectInfo()
        {
            throw new NotImplementedException();
        }

        public Task<ProjectInfo> GetProjectInfo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
