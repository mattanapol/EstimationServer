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
        public async Task<Material> CreateMaterial(int materialGroupId, Material material)
        {
            var projectMaterialGroup = await _projectMaterialGroupRepository.GetProjectMaterialGroup(materialGroupId);
            if (projectMaterialGroup == null)
                throw new ArgumentOutOfRangeException($"Project material group id = {materialGroupId} is not exist.");
            
            var projectMaterialDb = TypeMappingService.Map<Material, ProjectMaterialDb>(material);
            projectMaterialDb.MaterialGroupId = projectMaterialGroup.Id;
            projectMaterialDb.MaterialType = projectMaterialGroup.MaterialType;
            DbContext.Material.Add(projectMaterialDb);

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<ProjectMaterialDb, Material>(projectMaterialDb);
        }
    }
}
