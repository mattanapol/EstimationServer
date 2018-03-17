using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Interface.Repositories
{
    public interface ISubMaterialRepository
    {
        /// <summary>
        /// Create sub material
        /// </summary>
        /// <param name="mainMaterialId"></param>
        /// <param name="subMaterial"></param>
        /// <returns></returns>
        Task<MaterialInfo> CreateSubMaterial(int mainMaterialId, MaterialInfo subMaterial);

        /// <summary>
        /// Get sub material by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SubMaterial> GetSubMaterial(int id);

        /// <summary>
        /// Update sub material
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subMaterial"></param>
        /// <returns></returns>
        Task<MaterialInfo> UpdateSubMaterial(int id, MaterialInfo subMaterial);

        /// <summary>
        /// Delete sub material by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteSubMaterial(int id);
    }
}
