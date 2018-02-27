using Estimation.DataAccess.Models;
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
    public class SubMaterialRepository : BaseMaterialRepository, ISubMaterialRepository
    {
        private readonly IMainMaterialRepository _mainMaterialRepository;
        public SubMaterialRepository(MaterialDbContext materialDbContext,
                                  ITypeMappingService typeMappingService,
                                  IMainMaterialRepository mainMaterialRepository)
            : base(materialDbContext, typeMappingService)
        {
            _mainMaterialRepository = mainMaterialRepository ?? throw new ArgumentNullException(nameof(mainMaterialRepository));
        }

        /// <summary>
        /// Create sub material
        /// </summary>
        /// <param name="mainMaterialId"></param>
        /// <param name="subMaterial"></param>
        /// <returns></returns>
        public async Task<MaterialInfo> CreateSubMaterial(int mainMaterialId, MaterialInfo subMaterial)
        {
            var mainMaterial = await _mainMaterialRepository.GetMainMaterial(mainMaterialId);
            if (mainMaterial == null)
                throw new KeyNotFoundException($"Main material id = {mainMaterialId} is not exist.");

            if (subMaterial.Code <= 0)
                subMaterial.Code = await GetNextCode(mainMaterialId);
            var subMaterialDb = TypeMappingService.Map<MaterialInfo, SubMaterialDb>(subMaterial);
            subMaterialDb.MainMaterialId = mainMaterialId;
            subMaterialDb.MaterialType = mainMaterial.MaterialType;
            DbContext.SubMaterials.Add(subMaterialDb);

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<SubMaterialDb, SubMaterial>(subMaterialDb);
        }

        /// <summary>
        /// Delete sub material
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteSubMaterial(int id)
        {
            var materialDb = await DbContext.SubMaterials
                                             .AsNoTracking()
                                             .SingleOrDefaultAsync(s => s.Id == id);
            if (materialDb == null)
                return;
            DbContext.SubMaterials.Remove(materialDb);

            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get sub material
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SubMaterial> GetSubMaterial(int id)
        {
            SubMaterialDb subMaterialDb = await DbContext.SubMaterials
                .Include(s => s.Materials)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (subMaterialDb == null)
                throw new KeyNotFoundException($"Sub material id = {id} is not exist.");

            return TypeMappingService.Map<SubMaterialDb, SubMaterial>(subMaterialDb);
        }

        /// <summary>
        /// Update sub material
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subMaterial"></param>
        /// <returns></returns>
        public async Task<MaterialInfo> UpdateSubMaterial(int id, MaterialInfo subMaterial)
        {
            var subMaterialDb = await DbContext.SubMaterials
                                            .AsNoTracking()
                                            .SingleOrDefaultAsync(e => e.Id == id);
            if (subMaterialDb == null)
                throw new ArgumentOutOfRangeException(nameof(subMaterialDb), $"Sub material id = { id } does not exist.");

            
            subMaterialDb.Name = subMaterial.Name;
            subMaterialDb.Code = subMaterial.Code;
            DbContext.Entry(subMaterialDb).State = EntityState.Modified;
            DbContext.Entry(subMaterialDb).Property(e => e.CreatedDate).IsModified = false;
            DbContext.Entry(subMaterialDb).Property(e => e.Id).IsModified = false;

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<SubMaterialDb, MaterialInfo>(subMaterialDb);
        }

        /// <summary>
        /// Get next sub material code
        /// </summary>
        /// <returns></returns>
        private async Task<int> GetNextCode(int mainMaterialId)
        {
            var queryable = DbContext.SubMaterials
                .AsNoTracking()
                .Where(s => s.MainMaterialId == mainMaterialId);
            int maxCode = await queryable.AnyAsync() ? await queryable.MaxAsync(m => m.Code) : 0;
            return maxCode + 1;
        }
    }
}
