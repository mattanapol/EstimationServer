using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services.Interfaces.Repositories
{
    /// <summary>
    /// Interface of Project material repository
    /// </summary>
    public interface IProjectMaterialRepository
    {
        /// <summary>
        /// Create material and add to material group id accordingly
        /// </summary>
        /// <param name="materialGroupId"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        Task<Material> CreateMaterial(int materialGroupId, Material material);
    }
}
