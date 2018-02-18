using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services.Interfaces.Repositories
{
    public interface IMaterialRepository
    {
        /// <summary>
        /// Create main material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        Task<MaterialInfo> CreateMainMaterial(MaterialInfo material);

        /// <summary>
        /// Create sub material
        /// </summary>
        /// <param name="mainMaterialId"></param>
        /// <param name="subMaterial"></param>
        /// <returns></returns>
        Task<MaterialInfo> CreateSubMaterial(int mainMaterialId, MaterialInfo subMaterial);

        /// <summary>
        /// Create material
        /// </summary>
        /// <param name="subMaterialId"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        Task<Material> CreateMaterial(int subMaterialId, Material material);

        /// <summary>
        /// Get main material by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MaterialInfo> GetMainMaterial(int id);

        /// <summary>
        /// Get sub material by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MaterialInfo> GetSubMaterial(int id);

        /// <summary>
        /// Get all material list not include material details
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MainMaterial>> GetMaterialList();
    }
}
