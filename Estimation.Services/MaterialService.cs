using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;

        /// <summary>
        /// Material service constructor
        /// </summary>
        /// <param name="materialRepository"></param>
        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
        }

        /// <summary>
        /// Get overall materials including main and sub materials
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MainMaterial>> GetOverallMaterial(string materialType)
        {
            var materials = await _materialRepository.GetMaterialList(materialType);

            return materials;
        }

        /// <summary>
        /// Search material by type
        /// </summary>
        /// <param name="materialType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SearchResultMaterialDto>> SearchMaterialByType(string materialType)
        {
            var materials = await _materialRepository.GetMaterialList(materialType);

            List<SearchResultMaterialDto> materialsWithTags = new List<SearchResultMaterialDto>();

            foreach(var main in materials)
            {
                foreach(var sub in main.SubMaterials)
                {
                    foreach(var material in sub.Materials)
                    {
                        var materialWithTags = new SearchResultMaterialDto { Id = material.Id, Name = material.Name };
                        materialWithTags.AddTag(material.Name);
                        materialWithTags.AddTag(material.Description);
                        materialWithTags.AddTag(material.CodeAsString);
                        materialWithTags.AddTag(sub.Name);
                        materialWithTags.AddTag(main.Name);

                        materialsWithTags.Add(materialWithTags);
                    }
                }
            }

            return materialsWithTags;
        }
    }
}
