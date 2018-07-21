using AutoMapper;
using Estimation.DataAccess.Models;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;

namespace Estimation.Common.AutoMapper.Profiles
{
    /// <summary>
    /// Project info profile class
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class ProjectInfoProfile: Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInfoProfile"/> class.
        /// </summary>
        public ProjectInfoProfile()
        {
            CreateMap<ProjectInfoIncommingDto, ProjectInfo>()
                .ForMember(dest => dest.CreatedDate, opts => opts.Ignore())
                .ForMember(dest => dest.LastModifiedDate, opts => opts.Ignore());
            CreateMap<ProjectInfo, ProjectInfoLightDto>();

            CreateMap<ProjectInfo, ProjectInfoDb>()
                .ForMember(dest => dest.MiscellaneousManual, opts => opts.MapFrom(src => src.Miscellaneous.Manual))
                .ForMember(dest => dest.MiscellaneousPercentage, opts => opts.MapFrom(src => src.Miscellaneous.Percentage))
                .ForMember(dest => dest.MiscellaneousIsUsePercentage, opts => opts.MapFrom(src => src.Miscellaneous.IsUsePercentage))
                .ForMember(dest => dest.TransportationManual, opts => opts.MapFrom(src => src.Transportation.Manual))
                .ForMember(dest => dest.TransportationPercentage, opts => opts.MapFrom(src => src.Transportation.Percentage))
                .ForMember(dest => dest.TransportationIsUsePercentage, opts => opts.MapFrom(src => src.Transportation.IsUsePercentage));
            CreateMap<ProjectInfoDb, ProjectInfo>()
                .ForMember(dest => dest.Miscellaneous, opts => opts.MapFrom(src => new Cost() { Percentage = src.MiscellaneousPercentage, Manual = src.MiscellaneousManual, IsUsePercentage = src.MiscellaneousIsUsePercentage }))
                .ForMember(dest => dest.Transportation, opts => opts.MapFrom(src => new Cost() { Percentage = src.TransportationPercentage, Manual = src.TransportationManual, IsUsePercentage = src.TransportationIsUsePercentage }));

            CreateMap<ProjectInfo, ProjectInfoOutgoingDto>().ReverseMap();
        }
    }
}
