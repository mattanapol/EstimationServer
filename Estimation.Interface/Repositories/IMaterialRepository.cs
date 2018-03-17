using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Interface.Repositories
{
    public interface IMaterialRepository
    {
        
        /// <summary>
        /// Create material
        /// </summary>
        /// <param name="subMaterialId"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        Task<Material> CreateMaterial(int subMaterialId, Material material);

        /// <summary>
        /// Get material by material id.
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        Task<Material> GetMaterial(int materialId);

        /// <summary>
        /// Get all material list not include material details
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MainMaterial>> GetMaterialList();

        /// <summary>
        /// Update material
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        Task<Material> UpdateMaterial(int materialId, Material material);

        /// <summary>
        /// Delete material
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        Task DeleteMaterial(int materialId);
    }
}
