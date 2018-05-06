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
    /// Material profile class
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class MaterialProfile: Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialProfile"/> class.
        /// </summary>
        public MaterialProfile()
        {
            CreateMap<MaterialIncommingDto, Material>();

            CreateMap<MaterialInfo, MaterialDb>();
            CreateMap<MaterialDb, MaterialInfo>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.SubMaterial.MainMaterial.Code.ToString()}-{src.SubMaterial.Code.ToString("D2")}-{src.Code.ToString("D2")}"))
                .AfterMap((src, dest) => {
                    if (src.SubMaterial?.MainMaterial != null)
                        dest.Class = src.SubMaterial.MainMaterial.Class;
                });

            CreateMap<Material, MaterialDb>();
            CreateMap<MaterialDb, Material>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.SubMaterial.MainMaterial.Code.ToString()}-{src.SubMaterial.Code.ToString("D2")}-{src.Code.ToString("D2")}"))
                .AfterMap((src, dest) => {
                    if (src.SubMaterial?.MainMaterial != null)
                        dest.Class = src.SubMaterial.MainMaterial.Class;
                });
        }
    }
}
