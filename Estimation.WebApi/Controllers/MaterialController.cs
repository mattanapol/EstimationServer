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
        public async Task<IActionResult> GetAllMaterialList(Domain.Models.Type materialType)
        {
            IList<MainMaterial> materials = new List<MainMaterial>()
            {
                new MainMaterial{ Id = 1, Code = "401", Name = "Refrigerating System",
                    SubMaterials = new List<MaterialInfo>(){
                        new MaterialInfo { Id = 1, Code = "401-01", Name = "Water-Cooled Chiller" },
                        new MaterialInfo { Id = 2, Code = "401-02", Name = "Water-Cooled Chiller" }
                    }
                },
                new MainMaterial{ Id = 2, Code = "402", Name = "Refrigerating System",
                    SubMaterials = new List<MaterialInfo>(){
                        new MaterialInfo { Id = 3, Code = "402-01", Name = "Water-Cooled Chiller" },
                        new MaterialInfo { Id = 4, Code = "402-02", Name = "Water-Cooled Chiller" }
                    }
                }
            };
            
            return Ok(OutgoingResult<IList<MainMaterial>>.SuccessResponse(materials));
        }
    }
}