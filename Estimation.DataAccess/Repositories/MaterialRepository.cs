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
    public class MaterialRepository: BaseMaterialRepository, IMaterialRepository
    {
        public MaterialRepository(MaterialDbContext materialDbContext,
                                  ITypeMappingService typeMappingService) 
            : base(materialDbContext, typeMappingService)
        {
        }

        public async Task<MaterialInfo> CreateMainMaterial(MaterialInfo material)
        {
            // Need to check for material duplicate code

            // Add main material record
            var mainMaterialDb = TypeMappingService.Map<MaterialInfo, MainMaterialDb>(material);
            DbContext.MainMaterials.Add(mainMaterialDb);

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<MainMaterialDb, MaterialInfo>(mainMaterialDb);
        }



        public async Task<Material> CreateSubMaterial(int mainMaterialId, Material material)
        {
            var mainMaterial = await GetMainMaterial(mainMaterialId);
            
            var materialDb = TypeMappingService.Map<Material, MaterialDb>(material);
            materialDb.MainMaterialId = mainMaterialId;
            DbContext.Materials.Add(materialDb);

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<MaterialDb, Material>(materialDb);
        }

        public async Task<MaterialInfo> GetMainMaterial(int id)
        {
            MainMaterialDb mainMaterialDb = await DbContext.MainMaterials.FirstOrDefaultAsync(m => m.Id == id);
            if (mainMaterialDb == null)
                throw new KeyNotFoundException($"Main material id = {id} is not exist.");

            return TypeMappingService.Map<MainMaterialDb, MaterialInfo>(mainMaterialDb);
        }

        public async Task<IEnumerable<MainMaterial>> GetMaterialList()
        {
            IQueryable<MainMaterial> queryable = DbContext.MainMaterials
                .Include(c => c.SubMaterials)
                .Select(m => new MainMaterial
                { Id = m.Id, Code = m.Code, Name = m.Name, MaterialType = m.MaterialType,
                    SubMaterials = m.SubMaterials.Select(s => new MaterialInfo { Id = s.Id, Code = s.Code, Name = s.Name, MaterialType = s.MaterialType }) })
                .AsNoTracking();

            var results = queryable.ToArray();

            return results;
        }
    }
}
