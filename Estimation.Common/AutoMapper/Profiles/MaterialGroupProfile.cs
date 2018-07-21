using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Estimation.DataAccess.Models;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;

namespace Estimation.Common.AutoMapper.Profiles
{
    /// <summary>
    /// Material group profile class
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class MaterialGroupProfile: Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialGroupProfile"/> class.
        /// </summary>
        public MaterialGroupProfile()
        {
            CreateMap<ProjectMaterialGroupIncomingDto, ProjectMaterialGroup>();
            CreateMap<ProjectMaterialGroupUpdateIncomingDto, ProjectMaterialGroup>();

            CreateMap<ProjectMaterialGroup, MaterialGroupDb>()
                .ForMember(dest => dest.MiscellaneousManual, opts => opts.MapFrom(src => src.Miscellaneous.Manual))
                .ForMember(dest => dest.MiscellaneousPercentage, opts => opts.MapFrom(src => src.Miscellaneous.Percentage))
                .ForMember(dest => dest.MiscellaneousIsUsePercentage, opts => opts.MapFrom(src => src.Miscellaneous.IsUsePercentage))
                .ForMember(dest => dest.TransportationManual, opts => opts.MapFrom(src => src.Transportation.Manual))
                .ForMember(dest => dest.TransportationPercentage, opts => opts.MapFrom(src => src.Transportation.Percentage))
                .ForMember(dest => dest.TransportationIsUsePercentage, opts => opts.MapFrom(src => src.Transportation.IsUsePercentage))
                .ForMember(dest => dest.Materials, opts => opts.MapFrom(src => Mapper.Map<IEnumerable<ProjectMaterial>, List<ProjectMaterialDb>>(src.Materials)));
            CreateMap<MaterialGroupDb, ProjectMaterialGroup>()
                .ForMember(dest => dest.Miscellaneous, opts => opts.MapFrom(src => new Cost() { Percentage = src.MiscellaneousPercentage, Manual = src.MiscellaneousManual, IsUsePercentage = src.MiscellaneousIsUsePercentage }))
                .ForMember(dest => dest.Transportation, opts => opts.MapFrom(src => new Cost() { Percentage = src.TransportationPercentage, Manual = src.TransportationManual, IsUsePercentage = src.TransportationIsUsePercentage }));

            CreateMap<ProjectMaterialGroup, ProjectMaterialGroupOutgoingDto>().ReverseMap();
        }
    }
}
