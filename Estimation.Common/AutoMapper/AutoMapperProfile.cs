using AutoMapper;
using Estimation.DataAccess.Models;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Common.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapMainMaterials();
            MapSubMaterials();
            MapMaterials();
        }

        private void MapMaterials()
        {
            CreateMap<MaterialIncommingDto, Material>();
                //.ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.Code.ToString()));
                
            CreateMap<Material, MaterialDb>();
            CreateMap<MaterialDb, Material>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.SubMaterial.MainMaterial.Code.ToString()}-{src.SubMaterial.Code.ToString("D2")}-{src.Code.ToString("D2")}"));
        }

        private void MapMainMaterials()
        {
            CreateMap<MainMaterialIncommingDto, MaterialInfo>();

            CreateMap<MaterialInfo, MainMaterialDb>();
            CreateMap<MainMaterialDb, MaterialInfo>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.Code.ToString()}"));

            CreateMap<MainMaterial, MainMaterialDb>();
            CreateMap<MainMaterialDb, MainMaterial>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.Code.ToString()}"));
                //.ForMember(dest => dest.SubMaterials, opts => opts.MapFrom(src => src.SubMaterials));
        }

        private void MapSubMaterials()
        {
            CreateMap<SubMaterialIncommingDto, MaterialInfo>();

            CreateMap<MaterialInfo, SubMaterialDb>();
            CreateMap<SubMaterialDb, MaterialInfo>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.MainMaterial.Code.ToString()}-{src.Code.ToString("D2")}"));

            CreateMap<SubMaterial, SubMaterialDb>();
            CreateMap<SubMaterialDb, SubMaterial>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.MainMaterial.Code.ToString()}-{src.Code.ToString("D2")}"));
        }
    }
}
