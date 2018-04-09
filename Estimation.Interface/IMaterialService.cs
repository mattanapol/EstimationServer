using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Interface
{
    public interface IMaterialService
    {
        Task<IEnumerable<MainMaterial>> GetOverallMaterial(string materialType);

        Task<IEnumerable<SearchResultMaterialDto>> SearchMaterialByType(string materialType);
    }
}
