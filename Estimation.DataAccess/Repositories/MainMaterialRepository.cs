using Estimation.DataAccess.Models;
using Estimation.Domain.Models;
using Estimation.Services.Interfaces;
using Estimation.Services.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.DataAccess.Repositories
{
    /// <summary>
    /// Main material repository
    /// </summary>
    public class MainMaterialRepository : BaseMaterialRepository, IMainMaterialRepository
    {
        /// <summary>
        /// Main material repository
        /// </summary>
        /// <param name="materialDbContext"></param>
        /// <param name="typeMappingService"></param>
        public MainMaterialRepository(MaterialDbContext materialDbContext,
                                  ITypeMappingService typeMappingService)
            : base(materialDbContext, typeMappingService)
        {
        }

        /// <summary>
        /// Create main material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public async Task<MaterialInfo> CreateMainMaterial(MaterialInfo material)
        {
            // Need to check for material duplicate code

            // Add main material record
            if (material.Code <= 0)
                material.Code = await GetNextCode();
            var mainMaterialDb = TypeMappingService.Map<MaterialInfo, MainMaterialDb>(material);
            //mainMaterialDb.CodeAsString = mainMaterialDb.Code.ToString();

            DbContext.MainMaterials.Add(mainMaterialDb);

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<MainMaterialDb, MaterialInfo>(mainMaterialDb);
        }

        /// <summary>
        /// Delete main material
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteMainMaterial(int id)
        {
            var materialDb = await DbContext.MainMaterials
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(s => s.Id == id);
            if (materialDb == null)
                return;
            DbContext.MainMaterials.Remove(materialDb);

            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get main material
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MainMaterial> GetMainMaterial(int id)
        {
            MainMaterialDb mainMaterialDb = await DbContext.MainMaterials
                .Include(m => m.SubMaterials)
                .ThenInclude(s => s.Materials)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainMaterialDb == null)
                throw new KeyNotFoundException($"Main material id = {id} is not exist.");

            var mainMaterial = TypeMappingService.Map<MainMaterialDb, MainMaterial>(mainMaterialDb);
            //mainMaterial.CodeAsString = mainMaterialDb.Code.ToString();
            return mainMaterial;
        }

        /// <summary>
        /// Update main material
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mainMaterial"></param>
        /// <returns></returns>
        public async Task<MaterialInfo> UpdateMainMaterial(int id, MaterialInfo mainMaterial)
        {
            var mainMaterialDb = await DbContext.MainMaterials
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(e => e.Id == id);
            if (mainMaterialDb == null)
                throw new ArgumentOutOfRangeException(nameof(mainMaterialDb), $"Main material id = { id } does not exist.");


            mainMaterialDb.Name = mainMaterial.Name;
            mainMaterialDb.Code = mainMaterial.Code;
            mainMaterialDb.MaterialType = mainMaterial.MaterialType;
            DbContext.Entry(mainMaterialDb).State = EntityState.Modified;
            DbContext.Entry(mainMaterialDb).Property(e => e.CreatedDate).IsModified = false;
            DbContext.Entry(mainMaterialDb).Property(e => e.Id).IsModified = false;

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<MainMaterialDb, MaterialInfo>(mainMaterialDb);
        }

        /// <summary>
        /// Get next main material code
        /// </summary>
        /// <returns></returns>
        private async Task<int> GetNextCode()
        {
            var queryable = DbContext.MainMaterials;
            int maxCode = await queryable.AnyAsync() 
                ? await queryable.MaxAsync(m => m.Code) : 0;
            return maxCode + 1;
        }
    }
}
