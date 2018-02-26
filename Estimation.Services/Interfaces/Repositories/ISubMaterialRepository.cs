using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services.Interfaces.Repositories
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
        Task<MaterialInfo> GetSubMaterial(int id);
    }
}
