using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;

namespace Estimation.Common.AutoMapper.Profiles
{
    public class ProjectSummaryProfile: Profile
    {
        public ProjectSummaryProfile()
        {
            CreateMap<ProjectSummary, ProjectSummaryOutgoingDto>();
            CreateMap<GroupSummary, GroupSummaryOutgoingDto>();
        }
    }
}
