using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Interface
{
    public interface IProjectMaterialService
    {
        /// <summary>
        /// Get project material by project material id
        /// </summary>
        /// <param name="projectMaterialId"></param>
        /// <returns></returns>
        Task<ProjectMaterial> GetProjectMaterial(int projectMaterialId);
    }
}
