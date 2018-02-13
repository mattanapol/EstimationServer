using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using Kaewsai.Utilities.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estimation.WebApi.Controllers
{
    /// <summary>
    /// Material controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/material")]
    public class MaterialController : Controller
    {
        /// <summary>
        /// Get all materials list
        /// </summary>
        /// <returns></returns>
        [HttpGet("list/{materialType}")]
        public async Task<IActionResult> GetAllMaterialList(Domain.Models.Type materialType)
        {
            IList<MainMaterial> materials = new List<MainMaterial>()
            {
                new MainMaterial{ Id = 1, Code = "401", Name = "Refrigerating System", MaterialType = materialType,
                    SubMaterials = new List<MaterialInfo>(){
                        new MaterialInfo { Id = 1, Code = "401-01", Name = "Water-Cooled Chiller", MaterialType = materialType },
                        new MaterialInfo { Id = 2, Code = "401-02", Name = "Water-Cooled Chiller", MaterialType = materialType }
                    }
                },
                new MainMaterial{ Id = 2, Code = "402", Name = "Refrigerating System", MaterialType = materialType,
                    SubMaterials = new List<MaterialInfo>(){
                        new MaterialInfo { Id = 3, Code = "402-01", Name = "Water-Cooled Chiller", MaterialType = materialType },
                        new MaterialInfo { Id = 4, Code = "402-02", Name = "Water-Cooled Chiller", MaterialType = materialType }
                    }
                }
            };
            
            return Ok(OutgoingResult<IList<MainMaterial>>.SuccessResponse(materials));
        }

        /// <summary>
        /// Get material by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterial(int id)
        {
            Material material = new Material
            {
                Id = id,
                Name = "Water-Cooled Chiller",
                Code = "401-01",
                ListPrice = 10,
                NetPrice = 10,
                OfferPrice = 12,
                Manpower = 100,
                Fittings = 10,
                Supporting = 10,
                Painting = 10,
                Remark = "Noted as remarks"
            };

            return Ok(OutgoingResult<Material>.SuccessResponse(material));
        }
    }
}