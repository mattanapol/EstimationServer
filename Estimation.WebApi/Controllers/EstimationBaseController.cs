using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estimation.WebApi.Controllers
{
    /// <summary>
    /// Estimation base controller
    /// </summary>
    public class EstimationBaseController : Controller
    {
        /// <summary>
        /// Type Mapping service
        /// </summary>
        protected readonly ITypeMappingService TypeMappingService;

        /// <summary>
        /// Constructor of estimation base controller
        /// </summary>
        /// <param name="typeMappingService"></param>
        public EstimationBaseController(ITypeMappingService typeMappingService)
        {
            TypeMappingService = typeMappingService ?? throw new ArgumentNullException(nameof(typeMappingService));
        }
    }
}