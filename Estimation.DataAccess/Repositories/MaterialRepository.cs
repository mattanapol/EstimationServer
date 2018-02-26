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
        private readonly ISubMaterialRepository _subMaterialRepository;
        public MaterialRepository(MaterialDbContext materialDbContext,
                                  ITypeMappingService typeMappingService,
                                  ISubMaterialRepository subMaterialRepository) 
            : base(materialDbContext, typeMappingService)
        {
            _subMaterialRepository = subMaterialRepository ?? throw new ArgumentNullException(nameof(subMaterialRepository));
        }

        /// <summary>
        /// Create material
        /// </summary>
        /// <param name="subMaterialId"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        public async Task<Material> CreateMaterial(int subMaterialId, Material material)
        {
            var subMaterial = await _subMaterialRepository.GetSubMaterial(subMaterialId);

            var materialDb = TypeMappingService.Map<Material, MaterialDb>(material);
            materialDb.SubMaterialId = subMaterialId;
            materialDb.MaterialType = subMaterial.MaterialType;
            DbContext.Materials.Add(materialDb);

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<MaterialDb, Material>(materialDb);
        }

        /// <summary>
        /// Get material
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public async Task<Material> GetMaterial(int materialId)
        {
            var material = await DbContext.Materials
                                          .AsNoTracking()
                                          .SingleOrDefaultAsync(m => m.Id == materialId);
            return TypeMappingService.Map<MaterialDb, Material>(material);
        }

        /// <summary>
        /// Get material list
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MainMaterial>> GetMaterialList()
        {
            IQueryable<MainMaterial> queryable = DbContext.MainMaterials
                .Include(c => c.SubMaterials)
                .ThenInclude(c => c.Materials)
                .Select(m => new MainMaterial
                { Id = m.Id, Code = m.Code, Name = m.Name, MaterialType = m.MaterialType,
                    SubMaterials = m.SubMaterials.Select(s => new SubMaterial { Id = s.Id, Code = s.Code, Name = s.Name, MaterialType = s.MaterialType,
                        Materials = s.Materials.Select(c => new MaterialInfo { Id = c.Id, Code = c.Code, Name = c.Name, MaterialType = c.MaterialType })
                    })
                })
                .AsNoTracking();

            var results = await Task.Run(() => queryable.ToArray());

            return results;
        }

        /// <summary>
        /// Update material
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        public async Task<Material> UpdateMaterial(int materialId, Material material)
        {
            var materialDb = await DbContext.Materials
                                            .AsNoTracking()
                                            .SingleOrDefaultAsync(e => e.Id == materialId);
            if (materialDb == null)
                throw new ArgumentOutOfRangeException(nameof(materialId), $"Material id = { materialId } does not exist.");

            materialDb.ListPrice = material.ListPrice;
            materialDb.Manpower = material.Manpower;
            materialDb.Name = material.Name;
            materialDb.NetPrice = material.NetPrice;
            materialDb.OfferPrice = material.OfferPrice;
            materialDb.Painting = material.Painting;
            materialDb.Remark = material.Remark;
            materialDb.Supporting = material.Supporting;
            materialDb.Fittings = material.Fittings;
            materialDb.Code = material.Code;
            DbContext.Entry(materialDb).State = EntityState.Modified;
            DbContext.Entry(materialDb).Property(e => e.CreatedDate).IsModified = false;
            DbContext.Entry(materialDb).Property(e => e.Id).IsModified = false;

            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<MaterialDb, Material>(materialDb);
        }

        /// <summary>
        /// Delete material
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public async Task DeleteMaterial(int materialId)
        {
            var materialDb = await DbContext.Materials
                                             .AsNoTracking()
                                             .SingleOrDefaultAsync(s => s.Id == materialId);
            DbContext.Materials.Remove(materialDb);

            await DbContext.SaveChangesAsync();
        }
    }
}
