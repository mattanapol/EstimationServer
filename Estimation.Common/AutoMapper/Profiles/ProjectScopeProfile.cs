using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;

namespace Estimation.Common.AutoMapper.Profiles
{
    /// <summary>
    /// Project scope profile class
    /// </summary>
    /// <seealso cref="Profile" />
    public class ProjectScopeProfile: Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectScopeProfile"/> class.
        /// </summary>
        public ProjectScopeProfile()
        {
            CreateMap<ProjectScopeOfWorkGroup, ProjectScopeOfWorkGroupDto>().ReverseMap();
            CreateMap<ProjectScopeOfWork, ProjectScopeOfWorkDto>().ReverseMap();
        }
    }
}
