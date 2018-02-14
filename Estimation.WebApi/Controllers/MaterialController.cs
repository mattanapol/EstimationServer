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
        [HttpGet]
        public async Task<IActionResult> GetAllMaterialList()
        {
            IList<MainMaterial> materials = new List<MainMaterial>()
            {
                new MainMaterial{ Id = 1, Code = "401", Name = "Refrigerating System", MaterialType = Domain.Models.Type.Computer,
                    SubMaterials = new List<MaterialInfo>(){
                        new MaterialInfo { Id = 1, Code = "401-01", Name = "Water-Cooled Chiller", MaterialType = Domain.Models.Type.Computer },
                        new MaterialInfo { Id = 2, Code = "401-02", Name = "Water-Cooled Chiller", MaterialType = Domain.Models.Type.Computer }
                    }
                },
                new MainMaterial{ Id = 2, Code = "402", Name = "Refrigerating System", MaterialType = Domain.Models.Type.Electronic,
                    SubMaterials = new List<MaterialInfo>(){
                        new MaterialInfo { Id = 3, Code = "402-01", Name = "Water-Cooled Chiller", MaterialType = Domain.Models.Type.Electronic },
                        new MaterialInfo { Id = 4, Code = "402-02", Name = "Water-Cooled Chiller", MaterialType = Domain.Models.Type.Electronic }
                    }
                },
                new MainMaterial{ Id = 3, Code = "403", Name = "Refrigerating System", MaterialType = Domain.Models.Type.Mechanic,
                    SubMaterials = new List<MaterialInfo>(){
                        new MaterialInfo { Id = 5, Code = "403-01", Name = "Water-Cooled Chiller", MaterialType = Domain.Models.Type.Mechanic },
                        new MaterialInfo { Id = 6, Code = "403-02", Name = "Water-Cooled Chiller", MaterialType = Domain.Models.Type.Mechanic }
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