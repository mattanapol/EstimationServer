using AutoMapper;
using Estimation.DataAccess.Models;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;

namespace Estimation.Common.AutoMapper.Profiles
{
    /// <summary>
    /// Sub material profile class
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class SubMaterialProfile: Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubMaterialProfile"/> class.
        /// </summary>
        public SubMaterialProfile()
        {
            CreateMap<SubMaterialIncommingDto, MaterialInfo>();

            CreateMap<MaterialInfo, SubMaterialDb>();
            CreateMap<SubMaterialDb, MaterialInfo>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.MainMaterial.Code.ToString()}-{src.Code.ToString("D2")}"))
                .AfterMap((src, dest) => {
                    if (src.MainMaterial != null)
                        dest.Class = src.MainMaterial.Class;
                });

            CreateMap<SubMaterial, SubMaterialDb>();
            CreateMap<SubMaterialDb, SubMaterial>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.MainMaterial.Code.ToString()}-{src.Code.ToString("D2")}"))
                .AfterMap((src, dest) => {
                    if (src.MainMaterial != null)
                        dest.Class = src.MainMaterial.Class;
                    if (src.Materials == null)
                        dest.Materials = null;
                });
        }
    }
}
