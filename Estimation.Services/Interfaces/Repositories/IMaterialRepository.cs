using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services.Interfaces.Repositories
{
    public interface IMaterialRepository
    {
        Task<MaterialInfo> CreateMainMaterial(MaterialInfo material);

        Task<Material> CreateSubMaterial(int mainMaterialId, Material material);

        Task<Material> GetMaterial(int id);
    }
}
