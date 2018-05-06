using AutoMapper;
using Estimation.DataAccess.Models;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;

namespace Estimation.Common.AutoMapper.Profiles
{
    /// <summary>
    /// Main material profile class
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class MainMaterialProfile: Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMaterialProfile"/> class.
        /// </summary>
        public MainMaterialProfile()
        {
            CreateMap<MainMaterialIncommingDto, MaterialInfo>();

            CreateMap<MaterialInfo, MainMaterialDb>();
            CreateMap<MainMaterialDb, MaterialInfo>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.Code.ToString()}"));

            CreateMap<MainMaterial, MainMaterialDb>();

            CreateMap<MainMaterialDb, MainMaterial>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.Code.ToString()}"))
                .ForMember(dest => dest.SubMaterials, opts => opts.MapFrom(src => src.SubMaterials));
        }
    }
}
