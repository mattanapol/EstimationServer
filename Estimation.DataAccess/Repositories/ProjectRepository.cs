using Estimation.DataAccess.Models;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using Estimation.Services.Interfaces;
using Estimation.Services.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.DataAccess.Repositories
{
    /// <summary>
    /// Project repository class
    /// </summary>
    public class ProjectRepository : BaseProjectRepository, IProjectRepository
    {
        /// <summary>
        /// Project repository constructor
        /// </summary>
        /// <param name="projectDbContext"></param>
        /// <param name="typeMappingService"></param>
        public ProjectRepository(ProjectDbContext projectDbContext,
                                  ITypeMappingService typeMappingService)
            : base(projectDbContext, typeMappingService)
        {
        }

        /// <summary>
        /// Create project infomation record
        /// </summary>
        /// <param name="projectInfo"></param>
        /// <returns></returns>
        public async Task<ProjectInfo> CreateProjectInfo(ProjectInfo projectInfo)
        {
            // Add main material record
            var projectDb = TypeMappingService.Map<ProjectInfo, ProjectInfoDb>(projectInfo);

            DbContext.ProjectInfo.Add(projectDb);

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<ProjectInfoDb, ProjectInfo>(projectDb);
        }

        public async Task DeleteProjectInfo(int id)
        {
            var projectDb = await DbContext.ProjectInfo
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(s => s.Id == id);
            if (projectDb == null)
                return;
            DbContext.ProjectInfo.Remove(projectDb);

            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get all project information
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectInfoLightDto>> GetAllProjectInfo()
        {
            var projectInfoDbs = await DbContext.ProjectInfo
                .AsNoTracking()
                .Select(p => new ProjectInfoLightDto()
                {
                    Id = p.Id,
                    Code = p.Code,
                    LastModifiedDate = p.LastModifiedDate,
                    CreatedDate = p.CreatedDate,
                    Name = p.Name,
                    Owner = p.Owner,
                    SubmitBy = p.SubmitBy
                })
                .ToArrayAsync();
            return projectInfoDbs;
        }

        /// <summary>
        /// Get project information record by project id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProjectInfo> GetProjectInfo(int id)
        {
            ProjectInfoDb projectInfoDb = await DbContext.ProjectInfo
                .Include(p => p.MaterialGroups)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectInfoDb == null)
                throw new KeyNotFoundException($"Project id = {id} is not exist.");

            var result = TypeMappingService.Map<ProjectInfoDb, ProjectInfo>(projectInfoDb);
            return result;
        }

        /// <summary>
        /// Update project information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectInfo"></param>
        /// <returns></returns>
        public async Task<ProjectInfo> UpdateProjectInfo(int id, ProjectInfo projectInfo)
        {
            var projectInfoDb = await DbContext.ProjectInfo
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(e => e.Id == id);
            if (projectInfoDb == null)
                throw new ArgumentOutOfRangeException(nameof(id), $"Project id = { id } does not exist.");


            projectInfoDb.Name = projectInfo.Name;
            projectInfoDb.Code = projectInfo.Code;
            projectInfoDb.Remark = projectInfo.Remark;
            projectInfoDb.Owner = projectInfo.Owner;
            projectInfoDb.GeneralContractor = projectInfo.GeneralContractor;
            projectInfoDb.ConstructionTerm = projectInfo.ConstructionTerm;
            projectInfoDb.ConstructionPlace = projectInfo.ConstructionPlace;
            projectInfoDb.ConstructionScale = projectInfo.ConstructionScale;
            projectInfoDb.KindOfWork = projectInfo.KindOfWork;
            projectInfoDb.SubmitBy = projectInfo.SubmitBy;
            projectInfoDb.LabourCost = projectInfo.LabourCost;
            projectInfoDb.CurrencyUnit = projectInfo.CurrencyUnit;
            projectInfoDb.MiscellaneousManual = projectInfo.Miscellaneous.Manual;
            projectInfoDb.MiscellaneousPercentage = projectInfo.Miscellaneous.Percentage;
            projectInfoDb.MiscellaneousIsUsePercentage = projectInfo.Miscellaneous.IsUsePercentage;
            projectInfoDb.TransportationManual = projectInfo.Transportation.Manual;
            projectInfoDb.TransportationPercentage = projectInfo.Transportation.Percentage;
            projectInfoDb.TransportationIsUsePercentage = projectInfo.Transportation.IsUsePercentage;
            DbContext.Entry(projectInfoDb).State = EntityState.Modified;
            DbContext.Entry(projectInfoDb).Property(e => e.CreatedDate).IsModified = false;
            DbContext.Entry(projectInfoDb).Property(e => e.Id).IsModified = false;

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<ProjectInfoDb, ProjectInfo>(projectInfoDb);
        }
    }
}
