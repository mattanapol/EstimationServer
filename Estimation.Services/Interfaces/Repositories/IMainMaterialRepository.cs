using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services.Interfaces.Repositories
{
    public interface IMainMaterialRepository
    {
        /// <summary>
        /// Create main material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        Task<MaterialInfo> CreateMainMaterial(MaterialInfo material);

        /// <summary>
        /// Get main material by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MainMaterial> GetMainMaterial(int id);

        /// <summary>
        /// Update main material
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mainMaterial"></param>
        /// <returns></returns>
        Task<MaterialInfo> UpdateMainMaterial(int id, MaterialInfo mainMaterial);

        /// <summary>
        /// Delete main material by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteMainMaterial(int id);
    }
}
