using AutoMapper;
using Estimation.DataAccess.Models;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Common.AutoMapper.Profiles
{

    /// <summary>
    /// Project material profile class
    /// </summary>
    /// <seealso cref="Profile" />
    public class ProjectMaterialProfile: Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectMaterialProfile"/> class.
        /// </summary>
        public ProjectMaterialProfile()
        {
            CreateMap<ProjectMaterialDb, ProjectMaterial>();
            CreateMap<ProjectMaterial, ProjectMaterialDb>();

            CreateMap<ProjectMaterialIncomingDto, ProjectMaterial>();
        }
    }
}
