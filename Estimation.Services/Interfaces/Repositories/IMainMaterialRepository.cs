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
        Task<MaterialInfo> GetMainMaterial(int id);
    }
}
