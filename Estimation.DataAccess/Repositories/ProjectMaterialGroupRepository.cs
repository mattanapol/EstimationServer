using Estimation.DataAccess.Models;
using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estimation.DataAccess.Repositories
{
    public class ProjectMaterialGroupRepository : BaseProjectRepository, IProjectMaterialGroupRepository
    {
        private readonly IProjectRepository _projectRepository;

        /// <summary>
        /// Project material repository constructor
        /// </summary>
        /// <param name="projectDbContext"></param>
        /// <param name="typeMappingService"></param>
        /// <param name="projectRepository"></param>
        public ProjectMaterialGroupRepository(ProjectDbContext projectDbContext,
                                  ITypeMappingService typeMappingService,
                                  IProjectRepository projectRepository)
            : base(projectDbContext, typeMappingService)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        /// <summary>
        /// Create project material group by project id
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectInfo"></param>
        /// <returns></returns>
        public async Task<ProjectMaterialGroup> CreateProjectMaterialGroup(int projectId, ProjectMaterialGroup projectInfo)
        {
            var project = await _projectRepository.GetProjectInfo(projectId);
            if (project == null)
                throw new ArgumentOutOfRangeException($"Project id = {projectId} is not exist.");

            if (projectInfo.ParentGroupId.GetValueOrDefault(0) > 0 && projectInfo.ParentGroupId != null)
            {
                var parentMaterialGroup = await GetProjectMaterialGroup(projectInfo.ParentGroupId.Value);
                if (parentMaterialGroup.ProjectId != projectId)
                    throw new ArgumentException("ParentGroupId is not in the same project.");
                else if (parentMaterialGroup.Materials.Count > 0)
                    throw new ArgumentException("ParentGroupId is already has materials.");
            }

            // Default miscellaneous and transportation
            projectInfo.Miscellaneous = project.Miscellaneous;
            projectInfo.Transportation = project.Transportation;
            var materialGroupDb = TypeMappingService.Map<ProjectMaterialGroup, MaterialGroupDb>(projectInfo);
            materialGroupDb.ProjectId = projectId;
            DbContext.MaterialGroup.Add(materialGroupDb);

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<MaterialGroupDb, ProjectMaterialGroup>(materialGroupDb);
        }

        /// <summary>
        /// Delete project material group by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteProjectMaterialGroup(int id)
        {
            var projectMaterialDb = await DbContext.MaterialGroup
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(s => s.Id == id);
            if (projectMaterialDb == null)
                return;

            var childGroups = await DbContext.MaterialGroup
                                             .AsNoTracking()
                                             .Where(e => e.ParentGroupId == id)
                                             .ToListAsync();

            foreach (var childGroup in childGroups)
                await DeleteProjectMaterialGroup(childGroup.Id);

            DbContext.MaterialGroup.Remove(projectMaterialDb);

            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get all project material group by project id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectMaterialGroup>> GetAllProjectMaterialGroupInProject(int projectId)
        {
            var materialGroupDbs = await DbContext.MaterialGroup
                .Include(e => e.Materials)
                .AsNoTracking()
                .Where(m => m.ProjectId == projectId)
                .ToArrayAsync();

            foreach(var materialGroupDb in materialGroupDbs)
                materialGroupDb.Materials = materialGroupDb.Materials.OrderBy(e => e.CodeAsString);

            var materialGroups = TypeMappingService.Map<IEnumerable<MaterialGroupDb>, IEnumerable<ProjectMaterialGroup>>(materialGroupDbs);
            return materialGroups;
        }

        /// <summary>
        /// Get project material group by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProjectMaterialGroup> GetProjectMaterialGroup(int id)
        {
            var materialGroupDb = await DbContext.MaterialGroup
                .Include(e => e.Materials)
                .OrderBy(e => e.GroupCode)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (materialGroupDb == null)
                throw new ArgumentOutOfRangeException($"Project material group id = {id} is not exist.");

            materialGroupDb.Materials = materialGroupDb.Materials.OrderBy(e => e.CodeAsString);

            var materialGroup = TypeMappingService.Map<MaterialGroupDb, ProjectMaterialGroup>(materialGroupDb);
            return materialGroup;
        }

        /// <summary>
        /// Update project material group by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectInfo"></param>
        /// <returns></returns>
        public async Task<ProjectMaterialGroup> UpdateProjectMaterialGroup(int id, ProjectMaterialGroup projectInfo)
        {
            var projectMaterialGroupDb = await DbContext.MaterialGroup
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(e => e.Id == id);
            if (projectMaterialGroupDb == null)
                throw new ArgumentOutOfRangeException(nameof(id), $"Project group id = { id } does not exist.");


            projectMaterialGroupDb.GroupCode = projectInfo.GroupCode;
            projectMaterialGroupDb.GroupName = projectInfo.GroupName;
            if (projectInfo.Miscellaneous != null && projectInfo.Transportation != null)
            {
                projectMaterialGroupDb.MiscellaneousIsUsePercentage = projectInfo.Miscellaneous.IsUsePercentage;
                projectMaterialGroupDb.MiscellaneousManual = projectInfo.Miscellaneous.Manual;
                projectMaterialGroupDb.MiscellaneousPercentage = projectInfo.Miscellaneous.Percentage;
                projectMaterialGroupDb.TransportationIsUsePercentage = projectInfo.Transportation.IsUsePercentage;
                projectMaterialGroupDb.TransportationManual = projectInfo.Transportation.Manual;
                projectMaterialGroupDb.TransportationPercentage = projectInfo.Transportation.Percentage;
            }
            //projectMaterialGroupDb.ParentGroupId = projectInfo.ParentGroupId; //Todo: Need to discuss about whether should we allow to change this.
            DbContext.Entry(projectMaterialGroupDb).State = EntityState.Modified;
            DbContext.Entry(projectMaterialGroupDb).Property(e => e.CreatedDate).IsModified = false;
            DbContext.Entry(projectMaterialGroupDb).Property(e => e.Id).IsModified = false;

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<MaterialGroupDb, ProjectMaterialGroup>(projectMaterialGroupDb);
        }
    }
}
