using Estimation.Domain.Models;
using Estimation.Services.Interfaces;
using Estimation.Services.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.DataAccess.Repositories
{
    public class MaterialRepository: BaseMaterialRepository, IMaterialRepository
    {
        public MaterialRepository(MaterialDbContext materialDbContext,
                                  ITypeMappingService typeMappingService) 
            : base(materialDbContext, typeMappingService)
        {
        }

        public Task<Material> CreateMaterial(Material material)
        {
            throw new NotImplementedException();
        }

        public Task<Material> GetMaterial(int id)
        {
            throw new NotImplementedException();
        }
    }
}
