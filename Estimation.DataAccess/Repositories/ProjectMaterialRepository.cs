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
        readonly IProjectMaterialGroupRepository _projectMaterialGroupRepository;

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

        /// <summary>
        /// Delete project material by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteMaterial(int id)
        {
            var materialDb = await DbContext.Material
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(s => s.Id == id);
            if (materialDb == null)
                return;
            DbContext.Material.Remove(materialDb);

            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get project material
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update project material
        /// </summary>
        /// <param name="id"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        public async Task<ProjectMaterial> UpdateMaterial(int id, ProjectMaterial material)
        {
            var materialDb = await DbContext.Material
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(e => e.Id == id);
            if (materialDb == null)
                throw new ArgumentOutOfRangeException(nameof(id), $"Material id = { id } does not exist.");

            materialDb.ListPrice = material.ListPrice;
            materialDb.Manpower = material.Manpower;
            materialDb.Name = material.Name;
            materialDb.NetPrice = material.NetPrice;
            materialDb.OfferPrice = material.OfferPrice;
            materialDb.Accessory = material.Accessory;
            materialDb.Painting = material.Painting;
            materialDb.Remark = material.Remark;
            materialDb.Supporting = material.Supporting;
            materialDb.Fittings = material.Fittings;
            materialDb.Code = material.Code;
            materialDb.CodeAsString = material.CodeAsString;
            materialDb.Unit = material.Unit;
            materialDb.Quantity = material.Quantity;
            materialDb.Description = material.Description;
            DbContext.Entry(materialDb).State = EntityState.Modified;
            DbContext.Entry(materialDb).Property(e => e.CreatedDate).IsModified = false;
            DbContext.Entry(materialDb).Property(e => e.Id).IsModified = false;

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<ProjectMaterialDb, ProjectMaterial>(materialDb);
        }
    }
}
