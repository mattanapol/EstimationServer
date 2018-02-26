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

        public async Task<MaterialInfo> CreateSubMaterial(int mainMaterialId, MaterialInfo subMaterial)
        {
            var mainMaterial = await _mainMaterialRepository.GetMainMaterial(mainMaterialId);

            var subMaterialDb = TypeMappingService.Map<MaterialInfo, SubMaterialDb>(subMaterial);
            subMaterialDb.MainMaterialId = mainMaterialId;
            subMaterialDb.MaterialType = mainMaterial.MaterialType;
            DbContext.SubMaterials.Add(subMaterialDb);

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<SubMaterialDb, SubMaterial>(subMaterialDb);
        }

        public async Task<MaterialInfo> GetSubMaterial(int id)
        {
            SubMaterialDb subMaterialDb = await DbContext.SubMaterials.FirstOrDefaultAsync(m => m.Id == id);
            if (subMaterialDb == null)
                throw new KeyNotFoundException($"Sub material id = {id} is not exist.");

            return TypeMappingService.Map<SubMaterialDb, MaterialInfo>(subMaterialDb);
        }
    }
}
