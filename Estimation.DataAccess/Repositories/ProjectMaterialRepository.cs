using Estimation.DataAccess.Models;
using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.DataAccess.Repositories
{
    /// <summary>
    /// Project material repository
    /// </summary>
    public class ProjectMaterialRepository : BaseProjectRepository, IProjectMaterialRepository
    {
        IProjectMaterialGroupRepository _projectMaterialGroupRepository;

        public ProjectMaterialRepository(ProjectDbContext projectDbContext,
                                    ITypeMappingService typeMappingService,
                                    IProjectMaterialGroupRepository projectMaterialGroupRepository): base(projectDbContext, typeMappingService)
        {
            _projectMaterialGroupRepository = projectMaterialGroupRepository ?? throw new ArgumentNullException(nameof(projectMaterialGroupRepository));
        }

        /// <summary>
        /// Create material and add it to project accordingly
        /// </summary>
        /// <param name="materialGroupId"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        public async Task<ProjectMaterial> CreateMaterial(int materialGroupId, ProjectMaterial material)
        {
            var projectMaterialGroup = await _projectMaterialGroupRepository.GetProjectMaterialGroup(materialGroupId);
            if (projectMaterialGroup == null)
                throw new ArgumentOutOfRangeException($"Project material group id = {materialGroupId} is not exist.");
            
            var projectMaterialDb = TypeMappingService.Map<ProjectMaterial, ProjectMaterialDb>(material);
            projectMaterialDb.MaterialGroupId = projectMaterialGroup.Id;
            projectMaterialDb.MaterialType = projectMaterialGroup.MaterialType;
            DbContext.Material.Add(projectMaterialDb);

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<ProjectMaterialDb, ProjectMaterial>(projectMaterialDb);
        }

        public async Task<ProjectMaterial> GetProjectMaterial(int id)
        {
            var projectMaterialDb = await DbContext.Material
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (projectMaterialDb == null)
                throw new ArgumentOutOfRangeException($"Project material id = {id} is not exist.");

            var projectMaterial = TypeMappingService.Map<ProjectMaterialDb, ProjectMaterial>(projectMaterialDb);
            return projectMaterial;
        }
    }
}
