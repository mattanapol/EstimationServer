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
    public class MainMaterialRepository : BaseMaterialRepository, IMainMaterialRepository
    {
        public MainMaterialRepository(MaterialDbContext materialDbContext,
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

        public async Task<MaterialInfo> GetMainMaterial(int id)
        {
            MainMaterialDb mainMaterialDb = await DbContext.MainMaterials.FirstOrDefaultAsync(m => m.Id == id);
            if (mainMaterialDb == null)
                throw new KeyNotFoundException($"Main material id = {id} is not exist.");

            return TypeMappingService.Map<MainMaterialDb, MaterialInfo>(mainMaterialDb);
        }
    }
}
